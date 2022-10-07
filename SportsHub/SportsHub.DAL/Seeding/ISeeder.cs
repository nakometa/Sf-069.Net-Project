using SportsHub.DAL.Data;

namespace SportsHub.DAL.Seeding
{
    public interface ISeeder
    {
        Task SeedAsync(ApplicationDbContext context);
    }
}
