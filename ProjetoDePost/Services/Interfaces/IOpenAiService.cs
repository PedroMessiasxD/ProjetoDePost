using Microsoft.AspNetCore.Routing.Template;
using ProjetoDePost.Models;

namespace ProjetoDePost.Services.Interfaces
{
    public interface IOpenAiService
    {
        Task<string> GerarIdeiasDePostagem(string promptDescricao, string temaPrincipal, int frequencia);
    }
}
