using Microsoft.EntityFrameworkCore;
using SportsHub.DAL.Data;
using SportsHub.Domain.Repository;

namespace SportsHub.DAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        protected readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<TEntity?> FindByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        protected DbSet<TEntity> DbSet => _context.Set<TEntity>();
    }
}
