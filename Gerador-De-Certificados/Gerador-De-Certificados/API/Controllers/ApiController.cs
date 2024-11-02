using Gerador_De_Certificados.Database;
using Gerador_De_Certificados.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Gerador_De_Certificados.API.Services;

namespace Gerador_De_Certificados.API.Controllers
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

        [HttpPost("custom/post")]
        public async Task<IActionResult> CreateCertificado(Certificado certificado)
        {
            _context.Certificados.Add(certificado);
            await _context.SaveChangesAsync();

            // Enviar a mensagem para a fila RabbitMQ
            await _rabbitMqSender.SendToQueueAsync(certificado);

            return CreatedAtAction(nameof(GetCertificado), new { id = certificado.IdCertificado }, certificado);
        }

        [HttpGet("custom/getAll")]
        public async Task<ActionResult<IEnumerable<Certificado>>> GetCertificado()
        {
            return await _context.Certificados.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Certificado>> GetCertificado(int id)
        {
            var certificado = await _context.Certificados.FindAsync(id);

            if (certificado == null)
                return NotFound();

            return certificado;
        }
    }
}
