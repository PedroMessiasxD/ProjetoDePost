using Microsoft.AspNetCore.Authorization;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;
using System.Security.Claims;

namespace ProjetoDePost.Authorization
{
    /// <summary>
    /// Manipulador para verificar se o usuário possui permissão administrativa para uma empresa.
    /// </summary>

    public class EmpresaAdminAuthorizationHandler : AuthorizationHandler<EmpresaAdminRequirement, int>
    {
        private readonly IParticipanteEmpresaService _participanteEmpresaService;

        /// <summary>
        /// Construtor que injeta o repositório de ParticipanteEmpresa.
        /// </summary>
        public EmpresaAdminAuthorizationHandler(IParticipanteEmpresaService participanteEmpresaService)
        {
            _participanteEmpresaService = participanteEmpresaService;
        }

        /// <summary>
        /// Manipula a autorização para verificar se o usuário é administrador da empresa especificada.
        /// </summary>

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            EmpresaAdminRequirement requirement,
            int empresaId)
        {
            if (context.User == null || !context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                return;
            }

            var usuarioId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (usuarioId == null)
            {
                return;
            }

            // Aqui estamos tentando acessar o contexto da requisição
            if (context.Resource is HttpContext httpContext)
            {
                if (httpContext.Request.RouteValues.ContainsKey("empresaId"))
                {
                    var empresaIdFromRoute = httpContext.Request.RouteValues["empresaId"].ToString();

                    if (int.TryParse(empresaIdFromRoute, out var empresaIdParsed))
                    {
                        // Verifica se o usuário é administrador da empresa
                        if (await _participanteEmpresaService.UsuarioEhAdminEmpresaAsync(usuarioId, empresaIdParsed))
                        {
                            context.Succeed(requirement); // Usuário tem permissão
                        }
                    }
                }
            }
        }
    }
}