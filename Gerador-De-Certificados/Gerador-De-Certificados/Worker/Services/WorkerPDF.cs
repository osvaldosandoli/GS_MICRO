using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Gerador_De_Certificados.Database;
using Gerador_De_Certificados.Models;
using DinkToPdf.Contracts;
using System.Text.Json;
using DinkToPdf; // Para o contrato de serviço

public class WorkerPDF
{
    private readonly ApplicationDbContext _context;
    private readonly IConverter _pdfConverter;

    public WorkerPDF(ApplicationDbContext context, IConverter pdfConverter)
    {
        _context = context;
        _pdfConverter = pdfConverter;
    }

    public void Start()
    {
        var factory = new ConnectionFactory() { Uri = new Uri("amqp://rabbitmq") };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "diplomasQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var jsonMessage = Encoding.UTF8.GetString(body);
            var certificado = JsonSerializer.Deserialize<Certificado>(jsonMessage);

            // Aqui você gera o PDF e salva no banco de dados
            await GenerateAndSavePdf(certificado);
        };

        channel.BasicConsume(queue: "diplomasQueue", autoAck: true, consumer: consumer);

        Console.WriteLine("Worker iniciado. Aguardando mensagens...");
        Console.ReadLine(); // Para manter o worker em execução
    }
    private async Task GenerateAndSavePdf(Certificado certificado)
    {
        // Criar o conteúdo HTML substituindo os placeholders pelos valores do certificado
        var htmlContent = $@"
        <!DOCTYPE html>
        <html lang='pt-br'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Diploma</title>
            <style>
                @media print {{
                    @page {{
                        size: A4 landscape;
                        margin: 0;
                    }}
                    body {{
                        margin: 0;
                        padding: 0;
                    }}
                    body::before, body::after {{
                        display: none;
                    }}
                }}
                body {{
                    font-family: 'Times New Roman', Times, serif;
                    background-color: #f4f4f4;
                    margin: 0;
                    padding: 0;
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    height: 100vh;
                }}
                .diploma-container {{
                    width: 90%;
                    max-width: 1000px;
                    margin: 0 auto;
                    padding: 40px;
                    background-color: white;
                    box-shadow: none;
                }}
                .header {{
                    text-align: center;
                    font-size: 28px;
                    font-weight: bold;
                }}
                .sub-header {{
                    text-align: center;
                    font-size: 20px;
                    margin: 10px 0;
                }}
                .content {{
                    margin: 40px 0;
                    font-size: 18px;
                    line-height: 1.5;
                }}
                .signatures {{
                    margin-top: 50px;
                    display: flex;
                    justify-content: space-between;
                }}
                .signature {{
                    text-align: center;
                }}
                .signature p {{
                    margin: 5px 0;
                }}
                .date {{
                    text-align: center;
                    margin-top: 40px;
                    font-size: 18px;
                }}
            </style>
        </head>
        <body>
            <div class='diploma-container'>
                <div class='header'>Universidade de Programação</div>
                <div class='sub-header'>Certificado de Conclusão</div>
                <div class='content'>
                    <p>
                        Certificamos que <strong>{certificado.Nome}</strong>, {certificado.Nacionalidade}, natural do Estado de {certificado.Estado}, nascido em {certificado.DataNascimento.ToString("dd/MM/yyyy")}, RG {certificado.Documento}, concluiu em {certificado.DataConclusao.ToString("dd/MM/yyyy")} o curso de {certificado.Curso}, nível de especialização, com carga horária de {certificado.CargaHoraria} horas.
                    </p>
                    <p>
                        Este certificado é concedido em conformidade com o artigo 44, inciso 3353, da Lei 9394/96, e com a Resolução 
                        C.N.C./C.C.S. nº 01/07.
                    </p>
                </div>
                <div class='date'>São Paulo, {certificado.DataEmissao.ToString("dd/MM/yyyy")}</div>
                <div class='signatures'>
                    <div class='signature'>
                        <p><strong>{certificado.NomeAssinatura}</strong></p>
                        <p>{certificado.Cargo}</p>
                    </div>            
                </div>
            </div>
        </body>
        </html>";

        var pdf = new HtmlToPdfDocument()
        {
            GlobalSettings = {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Landscape,
            PaperSize = PaperKind.A4,
        },
            Objects = {
            new ObjectSettings()
            {
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8" }
            }
        }
        };

        var pdfBytes = _pdfConverter.Convert(pdf);

        // Criar um código de conversão baseado em algum método
        var codigoConversao = Guid.NewGuid().ToString(); // Você pode usar outro método se preferir

        // Defina o caminho do PDF usando o código de conversão
        var caminhoPDF = $"Caminho/para/o/pdf/{codigoConversao}.pdf"; // Ajuste o caminho conforme necessário

        // Salvar PDF no sistema de arquivos
        await File.WriteAllBytesAsync(caminhoPDF, pdfBytes);

        // Atualizar o campo CaminhoPDF do certificado com o código de conversão
        certificado.CaminhoPDF = caminhoPDF;

        _context.Certificados.Update(certificado);
        await _context.SaveChangesAsync();
        Console.WriteLine("update");

        Console.WriteLine("PDF gerado e salvo para o certificado ID: " + certificado.IdCertificado);
    }
}