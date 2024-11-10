using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;

namespace ProjetoDePost.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> CriarUsuarioAsync(Usuario usuario, UsuarioCreateDto usuarioCreateDto);
        Task<Usuario> AtualizarUsuarioAsync(Usuario usuario);
        Task<bool> DeletarUsuarioAsync(string id);
        Task<IEnumerable<UsuarioReadDto>> BuscarTodosUsuariosAsync();
        Task<Usuario> BuscarUsuarioPorIdAsync(string id);
        Task<bool> PromoverUsuarioAsync(string usuarioId);
        Task<bool> PromoverUsuarioPorEmailAsync(string email, int empresaId);
        Task<Usuario> BuscarUsuarioPorEmailAsync(string email);

    }
}
