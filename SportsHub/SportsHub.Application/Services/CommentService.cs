using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
using SportsHub.Api.Exceptions.CustomExceptionModels;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;
using SportsHub.Domain.Models.Constants;
using SportsHub.Domain.UOW;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

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
            var userExists = await _unitOfWork.UserRepository.GetByIdAsync(commentInput.AuthorId) != null;
            var articleExists = await _unitOfWork.ArticleRepository.GetByIdAsync(commentInput.ArticleId) != null;

            if (!userExists || !articleExists)
            {
                throw new NotFoundException("User or article not found!");
            }

            var comment = _mapper.Map<Comment>(commentInput);

            await _unitOfWork.CommentRepository.AddAsync(comment);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task LikeCommentAsync(LikeCommentDTO commentLike)
        {
            var existingLike = await GetLikeAsync(commentLike);

            if (existingLike == null)
            {
                var like = _mapper.Map<CommentLike>(commentLike);
                await _unitOfWork.CommentLikeRepository.AddAsync(like);
            }
            else
            {
                if (existingLike.IsLike == commentLike.IsLike)
                {
                    _unitOfWork.CommentLikeRepository.Delete(existingLike);
                }
                else
                {
                    existingLike.IsLike = commentLike.IsLike;
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<CommentLike?> GetLikeAsync(LikeCommentDTO commentLike)
        {
            var userAndCommentExist = await _unitOfWork.UserRepository.FindByIdAsync(commentLike.UserId) != null &&
                await _unitOfWork.CommentRepository.FindByIdAsync(commentLike.CommentId) != null;

            if (!userAndCommentExist)
            {
                throw new NotFoundException(string.Format(ExceptionMessages.NotFound, ExceptionMessages.CommentUser));
            }

            return await _unitOfWork.CommentLikeRepository.GetLikeAsync(commentLike.CommentId, commentLike.UserId);
        }
    }
}
