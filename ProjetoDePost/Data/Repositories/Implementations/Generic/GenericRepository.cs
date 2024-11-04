using Microsoft.EntityFrameworkCore;
using ProjetoDePost.Data.Repositories.Interfaces.Generic;

namespace ProjetoDePost.Data.Repositories.Implementations.Generic
{
    /// <summary>
    /// Repositório genérico para fornecer operações básicas de CRUD (Create, Read, Update, Delete) para qualquer entidade.
    /// Esta classe implementa a interface <see cref="IGenericRepository{T}"/> para realizar operações assíncronas no banco de dados.
    /// O repositório utiliza Entity Framework Core para interagir com o banco de dados e pode ser utilizado para diferentes
    /// tipos de entidades, sem duplicar código de operações comuns.
    ///
    /// A classe `GenericRepository<T>` fornece os seguintes métodos principais:
    /// - <see cref="CriarAsync(T)"/>: Adiciona uma nova entidade ao banco de dados.
    /// - <see cref="AtualizarAsync(T)"/>: Atualiza uma entidade existente no banco de dados.
    /// - <see cref="DeletarAsync(string)"/>: Remove uma entidade do banco de dados com base no ID.
    /// - <see cref="BuscarTodosAsync"/>: Retorna todas as entidades de um tipo específico.
    /// - <see cref="BuscarPorIdAsync(string)"/>: Busca uma entidade específica pelo ID.
    ///
    /// Esta implementação facilita a reutilização e a consistência das operações de dados,
    /// evitando duplicação de código e permitindo que o mesmo repositório seja utilizado para diversas entidades.
    /// </summary>

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ProjetoDePostContext _context;
        private readonly DbSet<T> _dbSet;


        public GenericRepository(ProjetoDePostContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task CriarAsync(T entidade)
        {
            await _dbSet.AddAsync(entidade);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(T entidade)
        {
            _dbSet.Update(entidade);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(string id)
        {
            var entidade = await _dbSet.FindAsync(id);
            if (entidade != null)
            {
                _dbSet.Remove(entidade);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> BuscarTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> BuscarPorIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

    }

}
