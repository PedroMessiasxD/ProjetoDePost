using ProjetoDePost.Models;

namespace ProjetoDePost.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task CriarUsuarioAsync(Usuario usuario);
        Task AtualizarUsuarioAsync(Usuario usuario);
        Task DeletarUsuarioAsync(string id);
        Task<IEnumerable<Usuario>> BuscarTodosUsuariosAsync();
        Task<Usuario> BuscarUsuarioPorIdAsync(string id);
    }
}
