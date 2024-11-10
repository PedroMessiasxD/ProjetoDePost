using Microsoft.AspNetCore.Mvc;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;

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

        // GET api/historicocampanha/{empresaId}
        [HttpGet("{empresaId}")]
        public async Task<ActionResult<HistoricoCampanha>> GetHistoricoPorEmpresa(int empresaId)
        {
            var historicoCampanha = await _historicoCampanhaService.ObterHistoricoPorEmpresaAsync(empresaId);

            if (historicoCampanha == null)
            {
                return NotFound($"Não foi encontrado histórico para a empresa com ID {empresaId}.");
            }

            return Ok(historicoCampanha);
        }
    }
}
