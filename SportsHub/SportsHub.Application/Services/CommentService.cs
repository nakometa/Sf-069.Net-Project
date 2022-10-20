﻿using SportsHub.Domain.Models;
using SportsHub.Domain.UOW;

namespace SportsHub.AppService.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Comment>> GetByArticleAsync(int id)
        {
            return await unitOfWork.CommentRepository.GetByArticleAsync(id);
        }

        public async Task<IEnumerable<Comment>> GetByAuthorAsync(int id)
        {
            return await unitOfWork.CommentRepository.GetByAuthorAsync(id);
        }

        public async Task<IEnumerable<Comment>> GetByAuthorAsync(string author)
        {
            return await unitOfWork.CommentRepository.GetByAuthorAsync(author);
        }
    }
}
