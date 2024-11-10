using ProjetoDePost.Models;

namespace ProjetoDePost.Services.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);
    }
}
