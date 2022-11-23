using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;

namespace SportsHub.AppService.Services
{
    public interface ISportService
    {
        Task<IEnumerable<Sport>> GetAllAsync();
        Task<Sport?> GetByIdAsync(int id);
        Task<Sport?> GetByNameAsync(string sport);
        Task<bool> CreateSportAsync(CreateSportDTO sportDTO);
        Task<bool> EditSportAsync(CreateSportDTO sportDTO);
        Task<bool> DeleteSportAsync(int id);
    }
}
