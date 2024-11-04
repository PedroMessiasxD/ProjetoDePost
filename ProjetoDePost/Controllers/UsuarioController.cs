/*using Microsoft.AspNetCore.Mvc;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;

namespace ProjetoDePost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CriarUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Usuário não pode ser nulo");
            }
            await _usuarioService.CriarUsuarioAsync(usuario);
            return CreatedAtAction(nameof(BuscarUsuarioPorId), new {id = usuario.Id}, usuario);
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarUsuario(string id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest("ID do usuário não corresponde.");
            }

            await _usuarioService.AtualizarUsuarioAsync(usuario);
            return NoContent(); // 204
        }

        /// <summary>
        /// Deleta um usuário pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarUsuario(string id)
        {
            await _usuarioService.DeletarUsuarioAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Busca todos os usuários.
        /// </summary>
        /// <returns>Uma lista de usuários.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> BuscarTodosUsuarios()
        {
            var usuarios = await _usuarioService.BuscarTodosUsuariosAsync();
            return Ok(usuarios); // 200
        }

        /// <summary>
        /// Busca um usuário pelo ID.
        /// </summary>
        /// <returns>O usuário encontrado ou NotFound se não for encontrado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> BuscarUsuarioPorId(string id)
        {
            var usuario = await _usuarioService.BuscarUsuarioPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound(); // 404
            }

            return Ok(usuario);
        }
    }
}
*/