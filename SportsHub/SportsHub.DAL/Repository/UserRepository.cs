﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task SaveUserAsync(User user)
        {
            await context.Users.AddAsync(user);
        }

        public async Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail)
        {
            return await context.Users
                .Where(x => x.Username == usernameOrEmail || x.Email == usernameOrEmail)
                .FirstOrDefaultAsync();
        }
    }
}
