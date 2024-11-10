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

        public AccountController(IUsuarioService usuarioService, UserManager<Usuario> userManager, IConfiguration configuration, IMapper mapper, ITokenService tokenService)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }



        ///<summary>
        /// Registrando um novo usuário
        /// </summary>
        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] UsuarioCreateDto usuarioCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var usuario = _mapper.Map<Usuario>(usuarioCreateDto);

            try
            {
                var usuarioCriado = await _usuarioService.CriarUsuarioAsync(usuario, usuarioCreateDto);
                return Ok("Usuário registrado com sucesso.");
            }
            catch (UsuarioCriacaoException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        ///<summary>
        /// Logando um usuário
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await _userManager.FindByEmailAsync(usuarioLoginDto.Email);
            if (usuario == null)
            {
                return Unauthorized("Credenciais inválidas.");
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(usuario, usuarioLoginDto.Senha);
            if (!passwordCheck)
            {
                return Unauthorized("Credenciais inválidas.");
            }

            var token = _tokenService.GerarToken(usuario);
            return Ok(new { Token = token });
        }
    }
}
