using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Exceptions;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;

namespace ProjetoDePost.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly TokenRevocationService _tokenRevocationService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<Usuario> _userManager;
        private readonly IUsuarioService _usuarioService;
        private readonly INotificacaoService _notificacaoService;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(TokenRevocationService tokenRevocationService, ITokenService tokenService, UserManager<Usuario> userManager,
            IUsuarioService usuarioService, INotificacaoService notificacaoService, IHttpContextAccessor httpContextAccessor,
            IUrlHelperFactory urlHelperFactory)
        {
            _tokenRevocationService = tokenRevocationService;
            _tokenService = tokenService;
            _userManager = userManager;
            _usuarioService = usuarioService;
            _notificacaoService = notificacaoService;
            _httpContextAccessor = httpContextAccessor;
            _urlHelperFactory = urlHelperFactory;
        }

        public void Logout(string token)
        {
            // Revoga o token
            _tokenRevocationService.RevokeToken(token);
        }

        public async Task<IActionResult> Login(UsuarioLoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Senha))
            {
                if (!user.EmailConfirmed)
                    return new UnauthorizedObjectResult(new { message = "E-mail não confirmado. Verifique sua caixa de entrada para confirmar seu e-mail." });

                bool isAdmin = await _userManager.IsInRoleAsync(user, "AdminGlobal") ||
                               await _userManager.IsInRoleAsync(user, "AdminEmpresarial");

                var userRoles = await _userManager.GetRolesAsync(user);

                var token = await _tokenService.GerarToken(user);
                

                return new OkObjectResult(new
                {
                    token,
                    roles = userRoles,
                    isAdmin,
                    userId = user.Id
                });
            }

            return new UnauthorizedObjectResult(new { message = "Credenciais inválidas." });
        }

        public async Task<IActionResult> ConfirmarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return new BadRequestObjectResult("E-mail inválido.");
            }

            var usuario = await _userManager.FindByEmailAsync(email);
            if (usuario == null)
            {
                return new BadRequestObjectResult("Usuário não encontrado.");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(usuario);
            var resultado = await _userManager.ConfirmEmailAsync(usuario, token);

            if (resultado.Succeeded)
            {
                return new OkObjectResult("E-mail confirmado com sucesso.");
            }
            else
            {
                return new BadRequestObjectResult("Erro ao confirmar e-mail.");
            }
        }

        public async Task<IActionResult> Registrar(UsuarioCreateDto model)
        {
            var usuario = new Usuario
            {
                UserName = model.Email,
                Email = model.Email,
                Nome = model.Nome
            };

            try
            {
                var usuarioCriado = await _usuarioService.CriarUsuarioAsync(usuario, model);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(usuario);
                var urlHelper = _urlHelperFactory.GetUrlHelper(new ActionContext(
                    _httpContextAccessor.HttpContext,
                    new RouteData(),
                    new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()));
                var confirmationLink = urlHelper.Action(nameof(ConfirmarEmail), "Account", new { token, email = usuario.Email }, _httpContextAccessor.HttpContext.Request.Scheme);

                if (confirmationLink == null)
                {
                    return new BadRequestObjectResult("Erro ao gerar o link de confirmação de e-mail.");
                }
                await _notificacaoService.EnviarEmailConfirmacaoAsync(usuario.Email, usuario.Nome, confirmationLink);
                return new OkObjectResult(new { message = "Usuário registrado com sucesso. Verifique seu e-mail para confirmação." });
            }
            catch (UsuarioCriacaoException ex)
            {
                return new BadRequestObjectResult(new { message = ex.Message });
            }
        }

    }
}
