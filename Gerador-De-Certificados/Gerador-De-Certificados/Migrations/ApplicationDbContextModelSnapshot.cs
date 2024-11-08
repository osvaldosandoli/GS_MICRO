﻿// <auto-generated />
using System;
using Gerador_De_Certificados.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gerador_De_Certificados.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gerador_De_Certificados.Models.Certificado", b =>
                {
                    b.Property<int>("IdCertificado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCertificado"));

                    b.Property<string>("CaminhoPDF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("CargaHoraria")
                        .HasColumnType("float");

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Curso")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("DataConclusao")
                        .HasColumnType("date");

                    b.Property<DateOnly>("DataEmissao")
                        .HasColumnType("date");

                    b.Property<DateOnly>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nacionalidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeAssinatura")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCertificado");

                    b.ToTable("Certificados");
                });
#pragma warning restore 612, 618
        }
    }
}
