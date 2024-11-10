using ProjetoDePost.Data.Repositories.Interfaces.Generic;
using ProjetoDePost.Models;

namespace ProjetoDePost.Data.Repositories.Interfaces
{
    public interface IEmpresaRepository : IGenericRepository<Empresa>
    {
        Task<Empresa> ObterPorIdAsync(int empresaId);
        Task<List<Empresa>> ListarTodasAsEmpresasAsync();
        Task<IEnumerable<Empresa>> BuscarTodosAsync(bool incluirParticipantes = false);

    }
}
