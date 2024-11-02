using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Gerador_De_Certificados.Models
{
    public class Certificado
    {
        [Key]
        public int IdCertificado { get; set; }

        public string Nacionalidade { get; set; }

        public string Estado { get; set; }

        public DateOnly DataNascimento { get; set; }

        public string Documento { get; set; }

        public DateTime DataConclusao { get; set; }

        public string Curso { get; set; }

        public double CargaHoraria { get; set; }

        public DateTime DataEmissao { get; set; }

        public string NomeAssinatura { get; set; }

        public string Cargo { get; set; }

        public string CaminhoPDF { get; set; }
    }
}
