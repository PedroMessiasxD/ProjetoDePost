using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoDePost.Models;

namespace ProjetoDePost.Data
{
    public class ProjetoDePostContext : IdentityDbContext
    {
        public ProjetoDePostContext(DbContextOptions<ProjetoDePostContext> options) :
            base(options)
        {

        }
        public DbSet<Postagem> Postagens { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ParticipanteEmpresa> ParticipanteEmpresas { get; set; }
        public DbSet<SolicitacaoCampanha> SolicitacoesCampanha { get; set; }
        public DbSet<Campanha> Campanhas { get; set; }
        public DbSet<SolicitacaoCadastroEmpresa> SolicitacoesCadastroEmpresa { get; set; }
        public DbSet<HistoricoCampanha> HistoricoCampanhas { get; set; }
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

            // Configuração de Empresa-Campanha (Um para Muitos)
            modelBuilder.Entity<Campanha>()
                .HasOne(p => p.Empresa)
                .WithMany(e => e.Campanhas)
                .HasForeignKey(p => p.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade); // Quando uma empresa for excluída, suas campanhas também são removidas

            // Configuração de Projeto-Postagem (Um para Muitos)
            modelBuilder.Entity<Postagem>()
                .HasOne(post => post.Campanha)
                .WithMany(proj => proj.Postagens)
                .HasForeignKey(post => post.CampanhaId)
                .OnDelete(DeleteBehavior.Cascade);
            // Configuração de Usuario - SolicitacaoCadastroEmpresa
            modelBuilder.Entity<SolicitacaoCadastroEmpresa>()
                .HasOne(s => s.Usuario)
                .WithMany()
                .HasForeignKey(s => s.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<SolicitacaoCadastroEmpresa>().ToTable("SolicitacoesCadastroEmpresa");

            modelBuilder.Entity<ParticipanteEmpresa>()
                .HasOne(pe => pe.Campanha)
                .WithMany(c => c.Participantes)
                .HasForeignKey(pe => pe.CampanhaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração de navegação entre ParticipanteEmpresa e Usuario
            modelBuilder.Entity<ParticipanteEmpresa>()
                .HasOne(pe => pe.Usuario)
                .WithMany(u => u.ParticipanteEmpresas)
                .HasForeignKey(pe => pe.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração da tabela para SolicitacaoCampanha
            modelBuilder.Entity<SolicitacaoCampanha>().ToTable("SolicitacoesCampanha");

            modelBuilder.Entity<HistoricoCampanha>()
                .HasOne(h => h.Campanha)
                .WithMany()
                .HasForeignKey(h => h.CampanhaId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
