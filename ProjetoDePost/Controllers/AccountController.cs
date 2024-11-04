using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ProjetoDePost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly UserManager<Usuario> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(IUsuarioService usuarioService, UserManager<Usuario> userManager, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
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

            if (usuarioCreateDto.Senha != usuarioCreateDto.ConfirmarSenha)
            {
                return BadRequest("A senha e a confirmação de senha devem ser iguais!");
            }

            var usuario = new Usuario
            {
                UserName = usuarioCreateDto.Email,
                Email = usuarioCreateDto.Email,
                Nome = usuarioCreateDto.Nome,
                Sobrenome = usuarioCreateDto.Sobrenome
            };

            var result = await _userManager.CreateAsync(usuario, usuarioCreateDto.Senha);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Usuário registrado com sucesso.");
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

            var token = GerarToken(usuario);
            return Ok(new { Token = token });
        }

        private string GerarToken (Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(ClaimTypes.Name, usuario.UserName)
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
