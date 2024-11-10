using Microsoft.EntityFrameworkCore;
using ProjetoDePost.Data.Repositories.Implementations.Generic;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Models;

namespace ProjetoDePost.Data.Repositories.Implementations
{
    public class PostagemRepository : GenericRepository<Postagem>, IPostagemRepository
    {
        private readonly ProjetoDePostContext _context;

        public PostagemRepository(ProjetoDePostContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Postagem>> BuscarPorCampanhaIdAsync(int campanhaId)
        {
            return await _context.Postagens
                .Where(p => p.CampanhaId == campanhaId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Postagem>> BuscarPorEmailAsync(string email)
        {
            // Busca o usuário pelo email
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario != null)
            {
                
                return await _context.Postagens
                    .Where(p => p.Campanha.Participantes.Any(pe => pe.UsuarioId == usuario.Id))
                    .ToListAsync();
            }

            return new List<Postagem>(); 
        }

        public async Task<List<Postagem>> ObterPostagensPorCampanhaEDataAsync(int campanhaId, DateTime data)
        {
            return await _context.Postagens
                .Where(p => p.CampanhaId == campanhaId && p.DataCriacao.Date == data.Date)
                .ToListAsync();
        }

        public async Task<Postagem> BuscarPorIdAsync(int id)
        {
            return await _context.Postagens
                .Include(p => p.Campanha)
                .FirstOrDefaultAsync(p => p.Id == id); 
        }

        public async Task DeletarAsync(int id)
        {
           
            var postagem = await _context.Postagens
                .FirstOrDefaultAsync(p => p.Id == id);

            if (postagem == null)
            {
                throw new KeyNotFoundException("Postagem não encontrada.");
            }

            
            _context.Postagens.Remove(postagem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Postagem>> ObterPostagensPorEmpresaIdAsync(int empresaId)
        {
            return await _context.Postagens
                .Where(p => p.Campanha.EmpresaId == empresaId)
                .ToListAsync();
        }

    }
}
