using Microsoft.EntityFrameworkCore;
using ProjetoDePost.Models;

namespace ProjetoDePost.Data
{
    /// <summary>
    /// Contexto de banco de dados para a aplicação ProjetoDePost.
    /// Define os DbSets para as entidades e configura as relações entre elas.
    /// </summary>
    public class ProjetoDePostContext : DbContext
    {
        public ProjetoDePostContext(DbContextOptions<ProjetoDePostContext> options) :
            base(options)
        {

        }
        public DbSet<Postagem> Postagens { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ParticipanteEmpresa> ParticipanteEmpresas { get; set; }
        public DbSet<Projeto> Projetos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configuração de Empresa-Administrador
            modelBuilder.Entity<Empresa>()
                .HasOne(e => e.Administrador)
                .WithMany()
                .HasForeignKey(e => e.AdministradorId)
                .OnDelete(DeleteBehavior.Restrict); // Define que a exclusão do administrador não exclui a empresa

           // Configuração de Empresa-Participantes(Muitos para Muitos)
            modelBuilder.Entity<ParticipanteEmpresa>()
                .HasOne(pe => pe.Usuario)
                .WithMany(u => u.ParticipanteEmpresas)
                .HasForeignKey(pe => pe.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ParticipanteEmpresa>()
                .HasOne(pe => pe.Empresa)
                .WithMany(e => e.Participantes)
                .HasForeignKey(pe => pe.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração de Empresa-Projeto (Um para Muitos)
            modelBuilder.Entity<Projeto>()
                .HasOne(p => p.Empresa)
                .WithMany(e => e.Projetos)
                .HasForeignKey(p => p.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração de Projeto-Postagem (Um para Muitos)
            modelBuilder.Entity<Postagem>()
                .HasOne(post => post.Projeto)
                .WithMany(proj => proj.Postagens)
                .HasForeignKey(post => post.ProjetoId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
