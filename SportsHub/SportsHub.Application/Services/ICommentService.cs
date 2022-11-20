using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;

namespace SportsHub.AppService.Services
{
    public interface ICommentService
    {
        IQueryable<Comment> GetByArticle(int id, CategoryParameters categoryParameters);
        Task<bool> AddCommentAsync(CreateCommentDTO commentInput);
    }
}
