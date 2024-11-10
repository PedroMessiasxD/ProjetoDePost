using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;
using System.Security.Claims;

namespace ProjetoDePost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly ISolicitacaoCadastroEmpresaService _solicitacaoCadastroEmpresaService;
        private readonly IEmpresaService _empresaService;

        public EmpresaController(ISolicitacaoCadastroEmpresaService solicitacaoCadastroEmpresaService, IEmpresaService empresaService)
        {
            _solicitacaoCadastroEmpresaService = solicitacaoCadastroEmpresaService;
            _empresaService = empresaService;
        }

        /// <summary>
        /// Solicita o cadastro de uma nova empresa.
        /// </summary>
        [HttpPost("solicitar-cadastro")]
        public async Task<IActionResult> SolicitarCadastro([FromBody] SolicitacaoCadastroEmpresaDto empresaDto)
        {

            Console.WriteLine(JsonConvert.SerializeObject(empresaDto));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "id_temporario"; // Por enquanto, não estou utilizando o JWT, então vai ser isso.


            if (string.IsNullOrEmpty(usuarioId))
            {
                return Unauthorized(new { Mensagem = "Usuário não autenticado." });
            }
            
            try
            {
                var resposta = await _solicitacaoCadastroEmpresaService.CriarSolicitacaoAsync(empresaDto, usuarioId);
                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Lista as solicitações de cadastro do usuário atual. Somente Admin Global pode acessar.
        /// </summary>
        [HttpGet("solicitacoes")]
        public async Task<IActionResult> ListarSolicitacoes()
        {
           
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            {
                var solicitacoes = await _solicitacaoCadastroEmpresaService.ObterSolicitacoesPendentesAsync();
                return Ok(solicitacoes);
            }
        }

        /// <summary>
        /// Aprova uma solicitação de cadastro. Somente Admin Global pode acessar.
        /// </summary>
        [HttpPost("aprovar-solicitacao/{solicitacaoId}")]
        public async Task<IActionResult> AprovarSolicitacao(int solicitacaoId)
        {
            try
            {
                var resultado = await _solicitacaoCadastroEmpresaService.AprovarSolicitacaoAsync(solicitacaoId);
                if (!resultado)
                {
                    return NotFound(new { Mensagem = "Solicitação não encontrada ou já aprovada." });
                }
                return Ok(new { Mensagem = "Solicitação aprovada e empresa criada com sucesso." });
            }
            catch (Exception ex)
            {
                // Adicionar log ou informações detalhadas sobre a exceção
                return BadRequest(new { Mensagem = ex.Message, Detalhes = ex.InnerException?.Message });

            }
        }

        /// <summary>
        /// Recusa uma solicitação de cadastro. Somente Admin Global pode acessar.
        /// </summary>
        [HttpPost("recusar-solicitacao/{solicitacaoId}")]
        public async Task<IActionResult> RecusarSolicitacao(int solicitacaoId)
        {
            try
            {
                var resultado = await _solicitacaoCadastroEmpresaService.RecusarSolicitacaoAsync(solicitacaoId);
                if (!resultado)
                {
                    return NotFound(new { Mensagem = "Solicitação não encontrada ou já recusada." });
                }
                return Ok(new { Mensagem = "Solicitação recusada com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Obtém todas as empresas. Somente Admin Global pode acessar.
        /// </summary>
        [HttpGet("todas")]
        public async Task<IActionResult> ObterTodasAsEmpresas()
        {
            try
            {
                var empresas = await _empresaService.ListarTodasAsEmpresasAsync();
                return Ok(empresas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }
    }
}