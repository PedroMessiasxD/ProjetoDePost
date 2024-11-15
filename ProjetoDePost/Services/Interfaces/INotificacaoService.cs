using ProjetoDePost.Models;

namespace ProjetoDePost.Services.Interfaces
{
    public interface INotificacaoService
    {
        Task EnviarPostagemEmailAsync(Campanha campanha, string conteudoGerado);
        Task EnviarEmailConfirmacaoAsync(string email, string nome, string token);

    }
}
