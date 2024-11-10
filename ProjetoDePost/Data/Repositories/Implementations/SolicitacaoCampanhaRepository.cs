using Microsoft.EntityFrameworkCore;
using ProjetoDePost.Data.Repositories.Implementations.Generic;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Models;

namespace ProjetoDePost.Data.Repositories.Implementations
{
    public class SolicitacaoCampanhaRepository : GenericRepository<SolicitacaoCampanha>, ISolicitacaoCampanhaRepository
    {
        private readonly ProjetoDePostContext _context;

        public SolicitacaoCampanhaRepository(ProjetoDePostContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SolicitacaoCampanha> BuscarPorIdAsync(int solicitacaoCampanhaId)
        {
            return await _context.SolicitacoesCampanha
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(sc => sc.Id == solicitacaoCampanhaId);
        }

        public async Task DeletarAsync(int solicitacaoCampanhaId)
        {
            var solicitacao = await _context.SolicitacoesCampanha.FindAsync(solicitacaoCampanhaId);
            if (solicitacao != null)
            {
                _context.SolicitacoesCampanha.Remove(solicitacao);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SolicitacaoCampanha>> ObterSolicitacoesReprovadasAsync()
        {
            return await _context.SolicitacoesCampanha
                .Where(s => !s.Aprovada) // Considerando que Aprovada = false significa reprovada
                .ToListAsync();
        }

        public async Task<IEnumerable<SolicitacaoCampanha>> ObterSolicitacoesPendentesAsync()
        {
            return await _context.SolicitacoesCampanha
                                 .Where(s => !s.Aprovada)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<SolicitacaoCampanha>> ObterSolicitacoesReprovadasPorEmpresaAsync(int empresaId)
        {
            return await _context.SolicitacoesCampanha
            .Where(s => s.EmpresaId == empresaId && s.Aprovada == false)
            .ToListAsync();
        }
    }
}
