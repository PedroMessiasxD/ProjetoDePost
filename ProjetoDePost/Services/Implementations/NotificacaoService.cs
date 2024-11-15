using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace ProjetoDePost.Services.Implementations
{
    public class NotificacaoService : INotificacaoService
    {
        private readonly string _sendGridApiKey;
        private readonly ILogger<NotificacaoService> _logger;

        public NotificacaoService(IConfiguration configuration, ILogger<NotificacaoService> logger)
        {
            _logger = logger;
            _sendGridApiKey = configuration["SendGrid:ApiKey"];
        }

        public async Task EnviarPostagemEmailAsync(Campanha campanha, string conteudoGerado)
        {
            if (string.IsNullOrWhiteSpace(_sendGridApiKey))
            {
                _logger.LogError("Chave de API do SendGrid não configurada.");
                throw new InvalidOperationException("Chave de API do SendGrid não configurada.");
            }

            var client = new SendGridClient(_sendGridApiKey);
            var assunto = $"Nova Postagem na Campanha {campanha.Nome}";

            foreach (var participante in campanha.Participantes)
            {
                var usuario = participante.Usuario;
                if (participante.Usuario?.Email == null) continue;


                var mensagemHtml = $"Olá {usuario.Nome}, <br><br>" +
                                   $"Temos novas postagens para a campanha <strong>{campanha.Nome}</strong> com o tema <strong>{campanha.TemaPrincipal}</strong>.<br><br>" +
                                   $"Conteúdo gerado: <br><p>{conteudoGerado}</p><br>" +
                                   $"Antenciosamente, <br>Equipe de Campanhas";

                var msg = new SendGridMessage
                {
                    From = new EmailAddress("pemlucena@gmail.com", "Empresa de Post"),
                    Subject = assunto,
                    HtmlContent = mensagemHtml
                };

                msg.AddTo(new EmailAddress(usuario.Email, usuario.Nome));

                var response = await client.SendEmailAsync(msg);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Falha ao enviar e-mail para {usuario.Email}");
                }
            }
        }
        public async Task EnviarEmailConfirmacaoAsync(string email, string nome, string confirmationLink)
        {
            if (string.IsNullOrWhiteSpace(_sendGridApiKey))
            {
                _logger.LogError("Chave de API do SendGrid não configurada.");
                throw new InvalidOperationException("Chave de API do SendGrid não configurada.");
            }

            var client = new SendGridClient(_sendGridApiKey);
            var assunto = "Confirmação de E-mail";

            var mensagemHtml = $"Olá {nome}, <br><br>" +
                               $"Por favor, clique no link abaixo para confirmar seu e-mail e ativar sua conta:<br><br>" +
                               $"<a href='{confirmationLink}'>Confirmar E-mail</a><br><br>" +
                               $"Caso não tenha solicitado este registro, por favor ignore este e-mail.<br><br>" +
                               $"Atenciosamente,<br>Equipe de Suporte";

            var msg = new SendGridMessage
            {
                From = new EmailAddress("pemlucena@gmail.com", "Empresa de Post"),
                Subject = assunto,
                HtmlContent = mensagemHtml
            };

            msg.AddTo(new EmailAddress(email, nome));

            var response = await client.SendEmailAsync(msg);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Falha ao enviar e-mail de confirmação para {email}");
            }
        }

    }
}
