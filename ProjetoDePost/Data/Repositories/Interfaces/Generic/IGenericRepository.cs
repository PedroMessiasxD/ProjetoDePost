namespace ProjetoDePost.Data.Repositories.Interfaces.Generic
{
    public interface IGenericRepository<T> where T : class
    {

        Task CriarAsync(T entidade);
        Task AtualizarAsync(T entidade);
        Task DeletarAsync(string id);
        Task<IEnumerable<T>> BuscarTodosAsync();
        Task<T> BuscarPorIdAsync(string id);

    }
}
