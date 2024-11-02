using Gerador_De_Certificados.Controllers;
using Gerador_De_Certificados.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Adicione o HttpClient ao cont�iner de servi�os
builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Adicione o servi�o RabbitMqSender aqui
builder.Services.AddScoped<RabbitMqSender>();
builder.Services.AddControllers(); // Se voc� estiver usando controladores

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Rota padr�o

// Rotas adicionais, se necess�rio
app.MapControllerRoute(
    name: "certificado",
    pattern: "certificado/{id}",
    defaults: new { controller = "Certificado", action = "Layout" }); // Exemplo de rota para certificado



app.Run();
