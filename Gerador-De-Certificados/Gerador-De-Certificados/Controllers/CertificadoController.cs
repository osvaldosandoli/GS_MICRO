using Gerador_De_Certificados.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gerador_De_Certificados.Controllers
{
    public class CertificadoController : Controller
    {
        private readonly HttpClient _httpClient;

        public CertificadoController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Layout(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7282/api/Api/{id}"); // Ajuste a URL conforme necessário
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var certificado = JsonSerializer.Deserialize<Certificado>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true,
                });

                return View(certificado);
            }

            return NotFound();
        }
    }
}
