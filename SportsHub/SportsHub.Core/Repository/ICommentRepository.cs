﻿using SportsHub.Domain.Models;

namespace SportsHub.Domain.Repository
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetByArticleAsync(int id);
        Task AddCommentAsync(Comment comment);
        Task LikeCommentAsync(int CommentId);
        Task DislikeCommentAsync(int CommentId);
    }
}
