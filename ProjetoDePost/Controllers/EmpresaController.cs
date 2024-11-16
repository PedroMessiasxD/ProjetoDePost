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

        [Authorize]
        [HttpPost("solicitar-cadastro")]
        public async Task<IActionResult> SolicitarCadastro([FromBody] SolicitacaoCadastroEmpresaDto empresaDto)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "id_temporario";

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

        [Authorize]
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