using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Gerador_De_Certificados.Models;

namespace Gerador_De_Certificados.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Certificado> Certificados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Certificado>()
                .HasKey(p => p.IdCertificado); // Define IdProduto como chave primária
        }
    }
}
