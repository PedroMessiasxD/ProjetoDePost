using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;

namespace ProjetoDePost.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de postagens, definindo métodos para operações relacionadas a postagens.
    /// </summary>
    public interface IPostagemService
    {
        Task<IEnumerable<PostagemReadDto>> BuscarPorCampanhaIdAsync(int campanhaId);
        Task<PostagemReadDto> ObterPostagemPorIdAsync(int id);
        Task<PostagemReadDto> CriarPostagemAsync(PostagemCreateDto postagemCreateDto);
        Task DeletarPostagemAsync(int id);
        Task<IEnumerable<PostagemReadDto>> ObterPostagensPorEmpresaAsync(int empresaId);
    }
}
