﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoDePost.Data;

#nullable disable

namespace ProjetoDePost.Migrations
{
    [DbContext(typeof(ProjetoDePostContext))]
    [Migration("20241105034510_D")]
    partial class D
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ProjetoDePost.Models.Campanha", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<int>("FrequenciaPostagem")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PublicoAlvo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("QuantidadeMaxPostagem")
                        .HasColumnType("int");

                    b.Property<string>("TemaPrincipal")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Campanhas");
                });

            modelBuilder.Entity("ProjetoDePost.Models.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdministradorId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LinksRedesSociais")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SetorAtuacao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("AdministradorId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("ProjetoDePost.Models.ParticipanteEmpresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("Papel")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("ParticipanteEmpresas");
                });

            modelBuilder.Entity("ProjetoDePost.Models.Postagem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Conteudo")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Postado")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ProjetoId")
                        .HasColumnType("int");

                    b.Property<string>("Tema")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TomLinguagem")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ProjetoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Postagens");
                });

            modelBuilder.Entity("ProjetoDePost.Models.SolicitacaoCadastroEmpresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Aprovado")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("DataSolicitacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DescricaoEmpresa")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EmailAdministrador")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LinkRedeSocial")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NomeAdministrador")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NomeEmpresa")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SetorAtuacao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TelefoneAdministrador")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("SolicitacoesCadastroEmpresa", (string)null);
                });

            modelBuilder.Entity("ProjetoDePost.Models.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool>("CadastroConfirmado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("EAdministradorEmpresa")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ProjetoDePost.Models.Campanha", b =>
                {
                    b.HasOne("ProjetoDePost.Models.Empresa", "Empresa")
                        .WithMany("Campanhas")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("ProjetoDePost.Models.Empresa", b =>
                {
                    b.HasOne("ProjetoDePost.Models.Usuario", "Administrador")
                        .WithMany()
                        .HasForeignKey("AdministradorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjetoDePost.Models.Usuario", null)
                        .WithMany("EmpresasCriadas")
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Administrador");
                });

            modelBuilder.Entity("ProjetoDePost.Models.ParticipanteEmpresa", b =>
                {
                    b.HasOne("ProjetoDePost.Models.Empresa", "Empresa")
                        .WithMany("Participantes")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoDePost.Models.Usuario", "Usuario")
                        .WithMany("ParticipanteEmpresas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ProjetoDePost.Models.Postagem", b =>
                {
                    b.HasOne("ProjetoDePost.Models.Campanha", "Campanha")
                        .WithMany("Postagens")
                        .HasForeignKey("ProjetoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoDePost.Models.Usuario", null)
                        .WithMany("Postagens")
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Campanha");
                });

            modelBuilder.Entity("ProjetoDePost.Models.Campanha", b =>
                {
                    b.Navigation("Postagens");
                });

            modelBuilder.Entity("ProjetoDePost.Models.Empresa", b =>
                {
                    b.Navigation("Campanhas");

                    b.Navigation("Participantes");
                });

            modelBuilder.Entity("ProjetoDePost.Models.Usuario", b =>
                {
                    b.Navigation("EmpresasCriadas");

                    b.Navigation("ParticipanteEmpresas");

                    b.Navigation("Postagens");
                });
#pragma warning restore 612, 618
        }
    }
}
