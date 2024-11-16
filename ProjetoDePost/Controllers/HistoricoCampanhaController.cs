using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;
using System.Security.Claims;

namespace ProjetoDePost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoCampanhaController : ControllerBase
    {
        private readonly IHistoricoCampanhaService _historicoCampanhaService;

        public HistoricoCampanhaController(IHistoricoCampanhaService historicoCampanhaService)
        {
            _historicoCampanhaService = historicoCampanhaService;
        }

        [Authorize]
        [HttpGet("{empresaId}")]
        public async Task<ActionResult<HistoricoCampanha>> GetHistoricoPorEmpresa(int empresaId)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
            var historicoCampanha = await _historicoCampanhaService.ObterHistoricoPorEmpresaAsync(empresaId,usuarioId);

            if (historicoCampanha == null)
            {
                return NotFound($"Não foi encontrado histórico para a empresa com ID {empresaId}.");
            }
            return Ok(historicoCampanha);

            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message); // Retorna 401 caso o usuário não tenha acesso
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar histórico: {ex.Message}");
            }
        }
    }
}
