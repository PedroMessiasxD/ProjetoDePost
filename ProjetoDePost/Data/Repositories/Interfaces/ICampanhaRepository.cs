using ProjetoDePost.Data.Repositories.Interfaces.Generic;
using ProjetoDePost.Models;

namespace ProjetoDePost.Data.Repositories.Interfaces
{
    public interface ICampanhaRepository : IGenericRepository<Campanha>
    {
        Task<Campanha> BuscarPorIdAsync(int id);
        Task<IEnumerable<Campanha>> ObterCampanhasPorEmpresaAsync(int empresaId);
        Task<IEnumerable<Campanha>> ObterCampanhasVinculadasAoParticipanteAsync(int participanteId);
        Task DeletarAsync(int id);
    }
}
