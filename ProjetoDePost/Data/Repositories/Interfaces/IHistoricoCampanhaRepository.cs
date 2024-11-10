using ProjetoDePost.Data.Repositories.Interfaces.Generic;
using ProjetoDePost.Models;

namespace ProjetoDePost.Data.Repositories.Interfaces
{
    public interface IHistoricoCampanhaRepository : IGenericRepository<HistoricoCampanha>
    {
        Task AdicionarHistoricoAsync(HistoricoCampanha historico);
        Task<List<HistoricoCampanha>> ObterHistoricoPorEmpresaAsync(int empresaId);
        Task AtualizarHistoricoAsync(HistoricoCampanha historicoCampanha);

    }
}
