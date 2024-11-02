using Gerador_De_Certificados.Database;
using Gerador_De_Certificados.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;
using static Gerador_De_Certificados.Controllers.ApiController;


namespace Gerador_De_Certificados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly RabbitMqSender _rabbitMqSender;

        public ApiController(ApplicationDbContext context, RabbitMqSender rabbitMqSender)
        {
            _context = context;
            _rabbitMqSender = rabbitMqSender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCertificado(Certificado certificado)
        {
            _context.Certificados.Add(certificado);
            await _context.SaveChangesAsync();

            // Enviar a mensagem para a fila RabbitMQ
            await _rabbitMqSender.SendToQueueAsync(certificado);

            return CreatedAtAction(nameof(GetCertificado), new { id = certificado.IdCertificado }, certificado);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Certificado>>> GetCertificado()
        {
            return await _context.Certificados.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Certificado>> GetCertificado(int id)
        {
            var produto = await _context.Certificados.FindAsync(id);

            if (produto == null)
                return NotFound();

            return produto;
        }

        public class RabbitMqSender
        {
            private const string QueueName = "diplomasQueue";
            private const string RabbitMqUri = "amqp://rabbitmq";

            public async Task SendToQueueAsync(object message)
            {
                try
                {
                    var factory = new ConnectionFactory() { Uri = new Uri(RabbitMqUri) };
                    using var connection = factory.CreateConnection();
                    using var channel = connection.CreateModel();

                    channel.QueueDeclare(queue: QueueName,
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "",
                                         routingKey: QueueName,
                                         basicProperties: properties,
                                         body: messageBody);

                    Console.WriteLine("Mensagem enviada para fila: " + JsonSerializer.Serialize(message));
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Erro ao enviar mensagem para fila: " + ex.Message);
                }
            }
        }
    }
}
