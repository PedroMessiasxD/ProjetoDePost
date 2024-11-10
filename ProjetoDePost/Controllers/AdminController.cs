using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoDePost.Services.Interfaces;
using ProjetoDePost.Models;
using ProjetoDePost.Data.DTOs;

namespace ProjetoDePost.Controllers
{
    //[Authorize(Policy = "AdminGlobal")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        

        public AdminController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
             
        }

        /// <summary>
        /// Lista todos os usuários no sistema.
        /// </summary>
        /// <returns>Uma lista de usuários.</returns>
        [HttpGet("usuarios")]
        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = await _usuarioService.BuscarTodosUsuariosAsync();
            return Ok(usuarios);
        }


        /// <summary>
        /// Promove um usuário a administrador.
        /// </summary>
        /// <returns>Uma resposta indicando o sucesso ou falha da operação.</returns>
        [HttpPost("promover-usuario-email")]
        public async Task<IActionResult> PromoverUsuario([FromBody] PromoverUsuarioRequestDto request)
        {
            try
            {
                var sucesso = await _usuarioService.PromoverUsuarioPorEmailAsync(request.Email, request.EmpresaId);
                if (sucesso)
                {
                    return Ok("Usuário promovido a administrador com sucesso.");
                }
                return BadRequest("Não foi possível promover o usuário.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}