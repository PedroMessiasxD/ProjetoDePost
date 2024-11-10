using Microsoft.EntityFrameworkCore;
using ProjetoDePost.Data.Repositories.Implementations.Generic;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;

namespace ProjetoDePost.Data.Repositories.Implementations
{
    public class SolicitacaoCadastroEmpresaRepository : GenericRepository<SolicitacaoCadastroEmpresa>, ISolicitacaoCadastroEmpresaRepository
    {
        private readonly ProjetoDePostContext _context;

        public SolicitacaoCadastroEmpresaRepository(ProjetoDePostContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SolicitacaoCadastroEmpresa>> ObterSolicitacoesPendentesAsync()
        {
            return await _context.SolicitacoesCadastroEmpresa
                .Where(s => s.Status == "Pendente")
                .ToListAsync();
        }

        public async Task AprovarSolicitacaoAsync(int solicitacaoId)
        {
            var solicitacao = await _context.SolicitacoesCadastroEmpresa.FindAsync(solicitacaoId);
            if (solicitacao != null && solicitacao.Status == "Pendente")
            {
                solicitacao.Status = "Aprovada";
                await _context.SaveChangesAsync();
            }
        }

        public async Task RecusarSolicitacaoAsync(int solicitacaoId)
        {
            var solicitacao = await _context.SolicitacoesCadastroEmpresa.FindAsync(solicitacaoId);
            if (solicitacao != null && solicitacao.Status == "Pendente")
            {
                solicitacao.Status = "Recusada";
                await _context.SaveChangesAsync();
            }
        }
    }
}
