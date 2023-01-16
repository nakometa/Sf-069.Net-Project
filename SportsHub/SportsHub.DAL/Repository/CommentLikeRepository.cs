using Microsoft.EntityFrameworkCore;
using SportsHub.DAL.Data;
using SportsHub.Domain.Models;
using SportsHub.Domain.Repository;

namespace SportsHub.DAL.Repository
{
    public class CommentLikeRepository : GenericRepository<CommentLike>, ICommentLikeRepository
    {
        public CommentLikeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CommentLike?> GetLikeAsync(int commentId, int userId)
        {
            return await DbSet
                .FirstOrDefaultAsync(x => x.CommentId == commentId && x.UserId == userId);
        }
    }
}
