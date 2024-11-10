using ProjetoDePost.Data.Repositories.Interfaces.Generic;
using ProjetoDePost.Models;

namespace ProjetoDePost.Data.Repositories.Interfaces
{
    /// <summary>
    /// Interface para o repositório de postagens, estendendo as operações genéricas com métodos específicos para postagens.
    /// </summary>
    public interface IPostagemRepository : IGenericRepository<Postagem>
    {
        Task<IEnumerable<Postagem>> BuscarPorCampanhaIdAsync(int campanhaId);
        Task<IEnumerable<Postagem>> BuscarPorEmailAsync(string email);
        Task<List<Postagem>> ObterPostagensPorCampanhaEDataAsync(int campanhaId, DateTime data);
        Task<Postagem> BuscarPorIdAsync(int id);
        Task DeletarAsync(int id);
    }
}
