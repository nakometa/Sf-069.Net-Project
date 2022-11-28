using Microsoft.EntityFrameworkCore;
using SportsHub.DAL.Data;
using SportsHub.Domain.Models;
using SportsHub.Domain.Repository;

namespace SportsHub.DAL.Repository
{
    public class SportRepository : GenericRepository<Sport>, ISportRepository
    {
        public SportRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Sport>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<Sport?> GetByIdAsync(int id)
        {
            return await DbSet
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }


        public async Task<Sport?> GetByNameAsync(string sport)
        {
            return await DbSet
                .Where(x => x.Name == sport)
                .FirstOrDefaultAsync();
        }

        public async Task AddSportAsync(Sport sport)
        {
            await _context.Sports.AddAsync(sport);
            _context.SaveChanges();
        }

        public void UpdateSport(Sport sport)
        {
            _context.Update(sport);
            _context.SaveChanges();
        }

        public void DeleteSport(Sport sport)
        {
            _context.Sports.Remove(sport);
            _context.SaveChanges();
        }
    }
}
