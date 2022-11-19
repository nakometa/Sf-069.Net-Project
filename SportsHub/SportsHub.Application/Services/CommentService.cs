using AutoMapper;
using SportsHub.Api.Exceptions.CustomExceptionModels;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;
using SportsHub.Domain.Models.Constants;
using SportsHub.Domain.UOW;

namespace SportsHub.AppService.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Comment>> GetByArticleAsync(int id)
        {
            return await _unitOfWork.CommentRepository.GetByArticleAsync(id) ??
                   throw new NotFoundException(string.Format(ExceptionMessages.NotFound, ExceptionMessages.Comment));
        }

        public async Task<bool> AddCommentAsync(CreateCommentDTO commentInput)
        {
            var userExists = _unitOfWork.UserRepository.GetById(commentInput.AuthorId) != null;
            var articleExists = _unitOfWork.ArticleRepository.GetById(commentInput.ArticleId) != null;

            if (!userExists || !articleExists)
            {
                throw new NotFoundException("User or article not found!");
            }

            var comment = _mapper.Map<Comment>(commentInput);

            await _unitOfWork.CommentRepository.AddAsync(comment);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> LikeCommentAsync(int commentId)
        {
            var comment = _unitOfWork.CommentRepository.GetById(commentId);

            if (comment == null)
            {
                throw new NotFoundException(string.Format(ExceptionMessages.NotFound, ExceptionMessages.Comment));
            }

            comment.Likes++;

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DislikeCommentAsync(int commentId)
        {
            var comment = _unitOfWork.CommentRepository.GetById(commentId);

            if (comment == null)
            {
                throw new NotFoundException(string.Format(ExceptionMessages.NotFound, ExceptionMessages.Comment));
            }

            comment.Dislikes++;

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
