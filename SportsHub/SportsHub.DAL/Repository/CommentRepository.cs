using Microsoft.EntityFrameworkCore;
using SportsHub.DAL.Data;
using SportsHub.Domain.Models;
using SportsHub.Domain.Repository;

namespace SportsHub.DAL.Repository
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Comment>> GetByArticleAsync(int id)
        {
            return await DbSet
                .Where(x => x.ArticleId == id)
                .ToListAsync();
        }

        public async Task PostCommentAsync(Comment comment)
        {
            await DbSet.AddAsync(comment);
        }
    }
}
