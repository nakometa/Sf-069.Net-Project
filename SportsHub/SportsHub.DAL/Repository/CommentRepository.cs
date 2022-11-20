﻿using Microsoft.EntityFrameworkCore;
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

        public IQueryable<Comment> GetByArticle(int id, CategoryParameters categoryParameters)
        {
            return DbSet
                .Where(x => x.ArticleId == id)
                .Skip((categoryParameters.PageNumber - 1) * categoryParameters.PageSize)
                .Take(categoryParameters.PageSize);
        }

        public IQueryable<Comment> OrderByDate(int id, CategoryParameters categoryParameters)
        {
            return DbSet
                .Where(x => x.ArticleId == id)
                .OrderBy(x => x)
                .Skip((categoryParameters.PageNumber - 1) * categoryParameters.PageSize)
                .Take(categoryParameters.PageSize);
        }
    }
}
