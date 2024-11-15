using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProjetoDePost.Data.Repositories.Implementations.Generic;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Models;

namespace ProjetoDePost.Data.Repositories.Implementations
{
    /// <summary>
    /// Esta classe é o repositório para manipulação de dados da entidade Usuário.
    /// Implementa a interface IUsuarioRepository e fornece métodos assíncronos 
    /// para realizar operações CRUD (Create, Read, Update, Delete) no banco de dados.
    /// Os métodos são implementados para lidar com a entidade Usuario,
    /// permitindo sua criação, atualização, deleção e busca.
    /// </summary>

    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly ProjetoDePostContext _context;
        
        public UsuarioRepository(ProjetoDePostContext context) : base(context)
        {
            _context = context;
            
        }

       

    }
}
