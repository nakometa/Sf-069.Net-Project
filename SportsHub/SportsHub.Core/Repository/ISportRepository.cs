using SportsHub.Domain.Models;

namespace SportsHub.Domain.Repository
{
    public interface ISportRepository
    {
        Task<IEnumerable<Sport>> GetAllAsync();
        Task<Sport?> GetByIdAsync(int id);
        Task<Sport?> GetByNameAsync(string sport);
        Task AddSportAsync(Sport sport);
        void UpdateSport(Sport sport);
        void DeleteSport(Sport sport);

    }
}
