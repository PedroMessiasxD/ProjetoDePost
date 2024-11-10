using Microsoft.AspNetCore.Authorization;

namespace ProjetoDePost.Authorization
{
    /// <summary>
    /// Representa um requisito de autorização para verificar se o usuário tem permissão de administrador em uma empresa específica.
    /// </summary>
    public class EmpresaAdminRequirement : IAuthorizationRequirement
    { 
        /// <summary>
        /// Inicializa uma nova instância de <see cref="EmpresaAdminRequirement"/>.
        /// </summary>       
        public EmpresaAdminRequirement()
        {
            
        }
    }
}
