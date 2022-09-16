namespace SportsHub.Domain.Repository
{
    public interface IGenericRepository<T>
        where T : class
    {
        T? GetById(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
    }
}
