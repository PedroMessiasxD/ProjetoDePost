using Microsoft.AspNetCore.Mvc;
using ProjetoDePost.Data.DTOs;

namespace ProjetoDePost.Services.Interfaces
{
    public interface IAuthService
    {
        void Logout(string token);
        Task<IActionResult> Login(UsuarioLoginDto model);
        Task<IActionResult> ConfirmarEmail(string email);
        Task<IActionResult> Registrar(UsuarioCreateDto model);
    }
}
