using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Services.Interfaces;
using System.Security.Claims;

namespace ProjetoDePost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "AdminEmpresarial")]
    public class AdminEmpresarialController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IParticipanteEmpresaService _participanteEmpresaService;
        private readonly ICampanhaService _campanhaService;
       
        public AdminEmpresarialController(IUsuarioService usuarioService, IParticipanteEmpresaService participanteEmpresaService, ICampanhaService campanhaService)
        {
            _usuarioService = usuarioService;
            _participanteEmpresaService = participanteEmpresaService;
            _campanhaService = campanhaService;
        }

        [HttpPost("associar-usuario")]
        public async Task<IActionResult> AssociarUsuarioNaEmpresa([FromBody] string email, [FromQuery] int empresaId)
        {
            try
            {
                var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var usuario = await _usuarioService.BuscarUsuarioPorIdAsync(usuarioId);

                var ehAdmin = await _participanteEmpresaService.VerificarSeUsuarioEhAdminEmpresarial(usuarioId, empresaId);
                if (!ehAdmin)
                {
                    return Unauthorized("Você não tem permissão para associar usuários nesta empresa.");
                }
               
                var usuarioRegistrado = await _usuarioService.BuscarUsuarioPorEmailAsync(email);
                if (usuarioRegistrado == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                var participanteEmpresaCreateDto = new ParticipanteEmpresaCreateDto
                {
                    UsuarioId = usuarioRegistrado.Id,
                    EmpresaId = empresaId,
                    Papel = "Membro"
                };

                await _participanteEmpresaService.AdicionarUsuarioNaEmpresaAsync(participanteEmpresaCreateDto);

                return Ok("Usuário associado à empresa com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao associar o usuário: {ex.Message}");
            }
        }

        [HttpPost("associar-usuario-campanha")]
        public async Task<IActionResult> AssociarUsuarioACampanha([FromBody] string email, [FromQuery] int empresaId, [FromQuery] int campanhaId)
        {
            try
            {     
                var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var usuario = await _usuarioService.BuscarUsuarioPorIdAsync(usuarioId);

                var ehAdmin = await _participanteEmpresaService.VerificarSeUsuarioEhAdminEmpresarial(usuarioId, empresaId);
                if (!ehAdmin)
                {
                    return Unauthorized("Você não tem permissão para associar usuários nesta empresa.");
                }

                var estaAssociado = await _participanteEmpresaService.VerificarSeUsuarioEstaAssociadoEmpresa(usuarioId, empresaId);
                if (!estaAssociado)
                {
                    return BadRequest("O usuario não está associado a esta empresa.");
                }
           
                var usuarioRegistrado = await _usuarioService.BuscarUsuarioPorEmailAsync(email);
                if (usuarioRegistrado == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                
                var participanteEmpresaCreateDto = new ParticipanteEmpresaCreateDto
                {
                    UsuarioId = usuarioRegistrado.Id,
                    EmpresaId = empresaId,
                    Papel = "Membro"
                };

      
                var participanteEmpresaReadDto = await _participanteEmpresaService.AdicionarParticipanteACampanhaAsync(participanteEmpresaCreateDto, campanhaId);

                return Ok(participanteEmpresaReadDto); 
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao associar o usuário à campanha: {ex.Message}");
            }
        }
            [HttpPost("solicitar-criacao-campanha")]
            public async Task<IActionResult> SolicitarCriacaoCampanha([FromBody] CampanhaCreateDto campanhaDto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _campanhaService.SolicitarCriacaoDeCampanha(campanhaDto);
                return Ok("Solicitação de criação de campanha enviada com sucesso.");
            }

            [HttpPost("aceitar-campanha/{solicitacaoId}")]
            public async Task<IActionResult> AceitarCampanha(int solicitacaoId,int empresaId)
            {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
                {
                    await _campanhaService.AceitarCampanha(solicitacaoId, usuarioId);
                    return Ok("Campanha aprovada e criada com sucesso.");
                }
                catch (KeyNotFoundException)
                {
                    return NotFound("Solicitação de campanha não encontrada.");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao aceitar a campanha: {ex.Message}");
                }
            }

            [HttpDelete("recusar-campanha/{solicitacaoId}")]
            public async Task<IActionResult> RecusarCampanha(int solicitacaoId, int empresaId)
            {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                try
                {
                    await _campanhaService.RecusarCampanha(solicitacaoId, usuarioId);
                    return Ok("Solicitação de campanha recusada e excluída com sucesso.");
                }
                catch (KeyNotFoundException)
                {
                    return NotFound("Solicitação de campanha não encontrada.");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao recusar a campanha: {ex.Message}");
                }
            }

        [HttpPut("abandonar/{id}")]
        public async Task<IActionResult> AbandonarCampanha(int id)
        {
            try
            {
                var campanha = await _campanhaService.AbandonarCampanha(id);
                return Ok(campanha);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Campanha não encontrada.");
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocorreu um erro ao abandonar a campanha.");
            }
        }
    }
    }
