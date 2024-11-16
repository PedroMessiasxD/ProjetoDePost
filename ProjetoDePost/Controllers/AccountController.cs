using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using ProjetoDePost.Exceptions;
using ProjetoDePost.Services.Implementations;
using Microsoft.AspNetCore.Authorization;

namespace ProjetoDePost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<Usuario> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;


        public AccountController(IUsuarioService usuarioService, UserManager<Usuario> userManager, 
            IConfiguration configuration, IMapper mapper, ITokenService tokenService, IAuthService authService)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] UsuarioCreateDto usuarioCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _authService.Registrar(usuarioCreateDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto model)
        {
            return await _authService.Login(model);
        }

        [HttpGet("confirmar-email")]
        public async Task<IActionResult> ConfirmarEmail(string email)
        {
             return await _authService.ConfirmarEmail(email);
        }
        
        [Authorize]
        [HttpPost("logout")]
        public ActionResult Logout([FromServices] AuthService authService)
        {          
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");          
            authService.Logout(token);
            return NoContent();
        }
    }
}
