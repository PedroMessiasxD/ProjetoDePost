using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Services.Interfaces;
using System.Security.Claims;

namespace ProjetoDePost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "AdminEmpresarial")]
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


        /// <summary>
        /// Associa um usuário registrado a uma empresa existente.
        /// </summary>
        [HttpPost("associar-usuario")]
        public async Task<IActionResult> AssociarUsuarioNaEmpresa([FromBody] string email, [FromQuery] int empresaId)
        {
            try
            {
               /* // Verifica se o usuário tem permissão para associar usuários na empresa
                var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var usuario = await _usuarioService.BuscarUsuarioPorIdAsync(usuarioId);

                // Verifica se o AdminEmpresarial está vinculado à empresa
                var ehAdmin = await _participanteEmpresaService.VerificarSeUsuarioEhAdminEmpresarial(usuarioId, empresaId);
                if (!ehAdmin)
                {
                    return Unauthorized("Você não tem permissão para associar usuários nesta empresa.");
                }
               */
                // Busca o usuário registrado pelo e-mail
                var usuarioRegistrado = await _usuarioService.BuscarUsuarioPorEmailAsync(email);
                if (usuarioRegistrado == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                // Associa o usuário registrado à empresa
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

        /// <summary>
        /// Associa um usuário a uma campanha dentro da empresa.
        /// </summary>
        [HttpPost("associar-usuario-campanha")]
        public async Task<IActionResult> AssociarUsuarioACampanha([FromBody] string email, [FromQuery] int empresaId, [FromQuery] int campanhaId)
        {
            try
            {
                /*
                // Verifica se o usuário tem permissão para associar usuários na empresa
                var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var usuario = await _usuarioService.BuscarUsuarioPorIdAsync(usuarioId);

                // Verifica se o AdminEmpresarial está vinculado à empresa
                var ehAdmin = await _participanteEmpresaService.VerificarSeUsuarioEhAdminEmpresarial(usuarioId, empresaId);
                if (!ehAdmin)
                {
                    return Unauthorized("Você não tem permissão para associar usuários nesta empresa.");
                }
                */
                // Busca o usuário registrado pelo e-mail
                var usuarioRegistrado = await _usuarioService.BuscarUsuarioPorEmailAsync(email);
                if (usuarioRegistrado == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                // Associa o usuário à campanha
                var participanteEmpresaCreateDto = new ParticipanteEmpresaCreateDto
                {
                    UsuarioId = usuarioRegistrado.Id,
                    EmpresaId = empresaId,
                    Papel = "Membro"
                };

                // Chama o serviço para associar o participante à campanha
                var participanteEmpresaReadDto = await _participanteEmpresaService.AdicionarParticipanteACampanhaAsync(participanteEmpresaCreateDto, campanhaId);

                return Ok(participanteEmpresaReadDto);  // Retorna o DTO criado
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao associar o usuário à campanha: {ex.Message}");
            }
        }

            /// <summary>
            /// Solicita a criação de uma nova campanha.
            /// </summary>
            [HttpPost("solicitar-criacao-campanha")]
            public async Task<IActionResult> SolicitarCriacaoCampanha([FromBody] CampanhaCreateDto campanhaDto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _campanhaService.SolicitarCriacaoDeCampanha(campanhaDto);
                return Ok("Solicitação de criação de campanha enviada com sucesso.");
            }

            /// <summary>
            /// Aprova uma solicitação de criação de campanha e cria a campanha.
            /// </summary>
            [HttpPost("aceitar-campanha/{solicitacaoId}")]
            public async Task<IActionResult> AceitarCampanha(int solicitacaoId)
            {
                try
                {
                    await _campanhaService.AceitarCampanha(solicitacaoId);
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

            /// <summary>
            /// Recusa uma solicitação de criação de campanha.
            /// </summary>
            [HttpDelete("recusar-campanha/{solicitacaoId}")]
            public async Task<IActionResult> RecusarCampanha(int solicitacaoId)
            {
                try
                {
                    await _campanhaService.RecusarCampanha(solicitacaoId);
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
        }
    }
