using Microsoft.EntityFrameworkCore;
using SportsHub.DAL.Data;
using SportsHub.Domain.Models;
using SportsHub.Domain.Repository;

namespace SportsHub.DAL.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await FindByIdAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task SaveUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail)
        {
            return await _context.Users
                .Where(x => x.Username == usernameOrEmail || x.Email == usernameOrEmail)
                .Include(x => x.Role)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteArticle(Article article)
        {
            await foreach (var contextUser in _context.Users)
            {
                if (contextUser.Articles.Any(x => x.Id == article.Id))
                {
                    contextUser.Articles.Remove(article);
                }
            }
        }
    }
}
