using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Services.Interfaces;

namespace ProjetoDePost.Controllers
{
    /// <summary>
    /// Controller para gerenciar as campanhas, incluindo operações de criação, leitura, atualização, exclusão e aprovação.
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]
    public class CampanhaController : ControllerBase
    {
        private readonly ICampanhaService _campanhaService;

        public CampanhaController(ICampanhaService campanhaService)
        {
            _campanhaService = campanhaService;
        }

        /// <summary>
        /// Cria uma nova campanha.
        /// </summary>
       
        [HttpPost]
        public async Task<IActionResult> CriarCampanha([FromBody] CampanhaCreateDto campanhaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var campanha = await _campanhaService.CriarCampanhaAsync(campanhaDto);
            return CreatedAtAction(nameof(ObterPorId), new { id = campanha.Id }, campanha);
        }
        /// <summary>
        /// Obtém uma campanha específica pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var campanha = await _campanhaService.ObterPorIdAsync(id);
            if (campanha == null)
                return NotFound("Campanha não encontrada.");

            return Ok(campanha);
        }

        /// <summary>
        /// Obtém todas as campanhas de uma empresa.
        /// </summary>
        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> ObterTodasPorEmpresa(int empresaId)
        {
            var campanhas = await _campanhaService.ObterTodasPorEmpresaAsync(empresaId);
            return Ok(campanhas.ToList());
        }

        /// <summary>
        /// Obtém campanhas vinculadas a um participante específico.
        /// </summary>
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

        /// <summary>
        /// Obtém todas as solicitações de campanha pendentes.
        /// </summary>
        [HttpGet("solicitacoes/pendentes")]
        public async Task<IActionResult> ObservarSolicitacoesCampanha()
        {
            var solicitacoesPendentes = await _campanhaService.ObservarSolicitacoesAsync();
            return Ok(solicitacoesPendentes);
        }


    }
}
