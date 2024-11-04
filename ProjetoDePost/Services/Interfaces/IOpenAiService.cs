using Microsoft.AspNetCore.Routing.Template;
using ProjetoDePost.Models;

namespace ProjetoDePost.Services.Interfaces
{
    public interface IOpenAiService
    {
        Task <Postagem> GerarPost(string tema, int quantidade);
    }
}
