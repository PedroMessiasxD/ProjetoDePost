
using ProjetoDePost.Data.Repositories.Interfaces.Generic;
using ProjetoDePost.Models;

namespace ProjetoDePost.Data.Repositories.Interfaces
{
    public interface ISolicitacaoCampanhaRepository : IGenericRepository<SolicitacaoCampanha>
    {
        Task<SolicitacaoCampanha> BuscarPorIdAsync(int solicitacaoCampanhaId);
        Task DeletarAsync(int solicitacaoCampanhaId);
        Task<IEnumerable<SolicitacaoCampanha>> ObterSolicitacoesReprovadasAsync();
        Task<IEnumerable<SolicitacaoCampanha>> ObterSolicitacoesPendentesAsync();

        Task<IEnumerable<SolicitacaoCampanha>> ObterSolicitacoesReprovadasPorEmpresaAsync(int empresaId);
    }
}
