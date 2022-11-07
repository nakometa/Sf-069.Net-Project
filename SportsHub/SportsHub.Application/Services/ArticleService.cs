using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;
using SportsHub.Domain.Models.Enumerations;
using SportsHub.Domain.UOW;

namespace SportsHub.AppService.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Article?> GetByTitleAsync(string title)
        {
            return await _unitOfWork.ArticleRepository.GetByTitleAsync(title);
        }

        public async Task<bool> CreateArticleAsync(CreateArticleDTO adminInput)
        {
            var articleExists = await GetByTitleAsync(adminInput.Title) != null;

            if (articleExists)
            {
                return false;
            }

            var article = new Article()
            {
                Title = adminInput.Title,
                CategoryId = adminInput.CategoryId,
                StateId = (int) StateEnums.Unpublished,
                Content = adminInput.Content,
                ArticlePicture = adminInput.ArticlePicture,
                CreatedOn = DateTime.UtcNow
            };

            await _unitOfWork.ArticleRepository.AddArticleAsync(article);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditArticle(CreateArticleDTO adminInput)
        {
            var article = GetByTitleAsync(adminInput.Title).GetAwaiter().GetResult();

            if(article == null)
            {
                return false;
            }

            article.CategoryId = adminInput.CategoryId;
            article.Content = adminInput.Content;

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
