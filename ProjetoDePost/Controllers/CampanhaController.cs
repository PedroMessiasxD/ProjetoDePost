using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Services.Interfaces;
using System.Security.Claims;

namespace ProjetoDePost.Controllers
{
   
    
    [Route("api/[controller]")]
    [ApiController]
    public class CampanhaController : ControllerBase
    {
        private readonly ICampanhaService _campanhaService;

        public CampanhaController(ICampanhaService campanhaService)
        {
            _campanhaService = campanhaService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarCampanha([FromBody] CampanhaCreateDto campanhaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var campanha = await _campanhaService.CriarCampanhaAsync(campanhaDto);
            return CreatedAtAction(nameof(ObterPorId), new { id = campanha.Id }, campanha);
        }
      
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var campanha = await _campanhaService.ObterPorIdAsync(id);

            if (campanha == null)
                return NotFound("Campanha não encontrada.");

            return Ok(campanha);
        }


        [Authorize]
        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> ObterTodasPorEmpresa(int empresaId)
        {
            try
            {
                var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var campanhas = await _campanhaService.ObterTodasPorEmpresaAsync(empresaId, usuarioId);
                return Ok(campanhas.ToList());
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar campanhas: {ex.Message}");
            }
        }

        [HttpGet("participante/{participanteId}")]
        public async Task<IActionResult> ObterVinculadasAoParticipante(int participanteId)
        {
            var campanhas = await _campanhaService.ObterVinculadasAoParticipanteAsync(participanteId);
            if (campanhas == null || !campanhas.Any())
            {
                return NotFound("Nenhuma campanha encontrada para o participante.");
            }
            return Ok(campanhas);
        }

        [Authorize]
        [HttpGet("solicitacoes/pendentes")]
        public async Task<IActionResult> ObservarSolicitacoesCampanha(int empresaId)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var solicitacoesPendentes = await _campanhaService.ObservarSolicitacoesAsync(usuarioId, empresaId);
                return Ok(solicitacoesPendentes);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao observar solicitações: {ex.Message}");
            }
        }


    }
}
