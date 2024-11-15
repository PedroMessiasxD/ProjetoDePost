using ProjetoDePost.Models;

namespace ProjetoDePost.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GerarToken(Usuario usuario);

    }
}
