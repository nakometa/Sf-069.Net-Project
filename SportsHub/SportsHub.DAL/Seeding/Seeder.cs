using SportsHub.DAL.Data;
using SportsHub.Domain.Models;
using System.Text.Json;

namespace SportsHub.DAL.Seeding
{
    public class Seeder : ISeeder
    {
        #region Raw Data
        private readonly string _roles = "[{\"Name\":\"User\"},{\"Name\":\"Admin\"}]";
        private readonly string _users = "[{\"Email\":\"admin@admin.com\",\"Username\":\"admin\",\"DisplayName\":\"Administrator\",\"Pseudonym\":\"Admin\",\"FirstName\":\"Admin\",\"LastName\":\"Admin\",\"Password\":\"@dm1n\",\"RoleId\":2},{\"Email\":\"notadmin@admin.com\",\"Username\":\"notadmin\",\"DisplayName\":\"NotAnAdministrator\",\"Pseudonym\":\"NotAdmin\",\"FirstName\":\"NotAdmin\",\"LastName\":\"NotAdmin\",\"Password\":\"n0t@dm1n\",\"RoleId\":1}]";
        private readonly string _states = "[{\"Name\":\"Published\"}, {\"Name\":\"Unpublished\"}, {\"Name\":\"Deleted\"}, {\"Name\":\"Draft\"}]";
        #endregion

        public async Task SeedAsync(ApplicationDbContext context)
        {
            await SeedRolesAsync(context);
            await SeedUsersAsync(context);
            await SeedStatesAsync(context);
        }

        private async Task SeedUsersAsync(ApplicationDbContext context)
        {
            IEnumerable<User> users = JsonSerializer.Deserialize<List<User>>(_users);

            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }

        private async Task SeedRolesAsync(ApplicationDbContext context)
        {
            IEnumerable<Role> roles = JsonSerializer.Deserialize<List<Role?>>(_roles);

            context.Roles.AddRange(roles);
            await context.SaveChangesAsync();
        }

        private async Task SeedStatesAsync(ApplicationDbContext context)
        {
            IEnumerable<State> states = JsonSerializer.Deserialize<List<State?>>(_states);

            context.States.AddRange(states);
            await context.SaveChangesAsync();
        }
    }
}
