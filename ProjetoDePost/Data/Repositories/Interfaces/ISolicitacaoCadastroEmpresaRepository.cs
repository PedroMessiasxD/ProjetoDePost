using ProjetoDePost.Data.Repositories.Interfaces.Generic;
using ProjetoDePost.Models;

namespace ProjetoDePost.Data.Repositories.Interfaces
{
    public interface ISolicitacaoCadastroEmpresaRepository : IGenericRepository<SolicitacaoCadastroEmpresa>
    {
        Task<IEnumerable<SolicitacaoCadastroEmpresa>> ObterSolicitacoesPendentesAsync();
        Task AprovarSolicitacaoAsync(int solicitacaoId);
        Task RecusarSolicitacaoAsync(int solicitacaoId);
    }
}
