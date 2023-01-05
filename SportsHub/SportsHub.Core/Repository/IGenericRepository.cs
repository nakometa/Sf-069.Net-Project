namespace SportsHub.Domain.Repository
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity?> FindByIdAsync(int id);
        Task<IEnumerable<TEntity>> FindAllAsync();
        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
    }
}
