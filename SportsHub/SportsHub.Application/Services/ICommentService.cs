using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;

namespace SportsHub.AppService.Services
{
    public interface ICommentService
    {
        IQueryable<Comment> GetByArticle(int id, CategoryParameters categoryParameters);
        Task<bool> AddCommentAsync(CreateCommentDTO commentInput);
        public IQueryable<Comment> GetByArticleOrderByDate(int id, CategoryParameters categoryParameters);
        public IQueryable<Comment> GetByArticleOrderByDateDescending(int id, CategoryParameters categoryParameters);
        Task<bool> LikeCommentAsync(int commentId);
        Task<bool> DislikeCommentAsync(int commentId);
    }
}
