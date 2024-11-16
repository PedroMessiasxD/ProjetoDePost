using Microsoft.AspNetCore.Authorization;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;
using System.Security.Claims;

namespace ProjetoDePost.Authorization
{


    public class EmpresaAdminAuthorizationHandler : AuthorizationHandler<EmpresaAdminRequirement, int>
    {
        private readonly IParticipanteEmpresaService _participanteEmpresaService;

 
        public EmpresaAdminAuthorizationHandler(IParticipanteEmpresaService participanteEmpresaService)
        {
            _participanteEmpresaService = participanteEmpresaService;
        }


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

            if (context.Resource is HttpContext httpContext)
            {
                if (httpContext.Request.RouteValues.ContainsKey("empresaId"))
                {
                    var empresaIdFromRoute = httpContext.Request.RouteValues["empresaId"].ToString();

                    if (int.TryParse(empresaIdFromRoute, out var empresaIdParsed))
                    {
                        
                        if (await _participanteEmpresaService.UsuarioEhAdminEmpresaAsync(usuarioId, empresaIdParsed))
                        {
                            context.Succeed(requirement);
                        }
                    }
                }
            }
        }
    }
}