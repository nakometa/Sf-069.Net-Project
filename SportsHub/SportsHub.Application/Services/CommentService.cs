using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;
using SportsHub.Domain.UOW;

namespace SportsHub.AppService.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Comment>> GetByArticleAsync(int id)
        {
            return await _unitOfWork.CommentRepository.GetByArticleAsync(id);
        }

        public async Task<bool> AddCommentAsync(CreateCommentDTO commentInput)
        {
            var userExists = _unitOfWork.UserRepository.GetById(commentInput.AuthorId) != null;
            var articleExists = _unitOfWork.ArticleRepository.GetById(commentInput.ArticleId) != null;

            if (!userExists || !articleExists)
            {
                return false;
            }

            var comment = new Comment()
            {
                Content = commentInput.Content,
                AuthorId = commentInput.AuthorId,
                ArticleId = commentInput.ArticleId,
            };

            await _unitOfWork.CommentRepository.AddCommentAsync(comment);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
