﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoDePost.Services.Interfaces;
using ProjetoDePost.Models;
using ProjetoDePost.Data.DTOs;
using System.Security.Claims;

namespace ProjetoDePost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ISolicitacaoCadastroEmpresaService _solicitacaoCadastroEmpresaService;
        

        public AdminController(IUsuarioService usuarioService, ISolicitacaoCadastroEmpresaService solicitacaoCadastroEmpresaService)
        {
            _usuarioService = usuarioService;
            _solicitacaoCadastroEmpresaService = solicitacaoCadastroEmpresaService;
             
        }
        [HttpGet("usuarios")]
        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = await _usuarioService.BuscarTodosUsuariosAsync();
            return Ok(usuarios);
        }

        [Authorize(Policy = "AdminGlobal")]
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
        
        [Authorize(Policy = "AdminGlobal")]
        [HttpGet("solicitacoes")]
        public async Task<IActionResult> ListarSolicitacoes()
        {

            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            {
                var solicitacoes = await _solicitacaoCadastroEmpresaService.ObterSolicitacoesPendentesAsync();
                return Ok(solicitacoes);
            }
        }
        
        [Authorize(Policy = "AdminGlobal")]
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
                
                return BadRequest(new { Mensagem = ex.Message, Detalhes = ex.InnerException?.Message });

            }
        }

        [Authorize(Policy = "AdminGlobal")]
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
    }
}