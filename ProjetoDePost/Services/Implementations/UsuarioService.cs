using ProjetoDePost.Data.Repositories.Interfaces.Generic;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;

namespace ProjetoDePost.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        
        public UsuarioService(IGenericRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        public async Task CriarUsuarioAsync(Usuario usuario)
        {
            await _usuarioRepository.CriarAsync(usuario);
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
            await _usuarioRepository.AtualizarAsync(usuario);
        }

        /// <summary>
        /// Deleta um usuário pelo ID.
        /// </summary>
        public async Task DeletarUsuarioAsync(string id)
        {
            await _usuarioRepository.DeletarAsync(id);
        }

        /// <summary>
        /// Busca todos os usuários.
        /// </summary>
        /// <returns>Uma lista de usuários.</returns>
        public async Task<IEnumerable<Usuario>> BuscarTodosUsuariosAsync()
        {
            return await _usuarioRepository.BuscarTodosAsync();
        }

        /// <summary>
        /// Busca um usuário pelo ID.
        /// </summary>
        /// <returns>O usuário encontrado.</returns>
        public async Task<Usuario> BuscarUsuarioPorIdAsync(string id)
        {
            return await _usuarioRepository.BuscarPorIdAsync(id);
        }
    }
}
