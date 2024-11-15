using System.Collections.Concurrent;

namespace ProjetoDePost.Services.Implementations
{
    /// <summary>
    /// Serviço de Revogação de TOKEN
    /// </summary>
    public class TokenRevocationService
    {
        private readonly ConcurrentDictionary<string, DateTime> revokedTokens = new();

        // Método para revogar um token
        public void RevokeToken(string token)
        {
            revokedTokens[token] = DateTime.UtcNow;
        }

        // Método para verificar se um token foi revogado
        public bool IsTokenRevoked(string token)
        {
            return revokedTokens.ContainsKey(token);
        }
    }
}
