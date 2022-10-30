using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;
using SportsHub.Domain.UOW;

namespace SportsHub.AppService.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork unitOfWork;

        public ArticleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Article?> GetByTitleAsync(string title)
        {
            return await unitOfWork.ArticleRepository.GetByTitleAsync(title);
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
                StateId = adminInput.StateId,
                Content = adminInput.Content,
                ArticlePicture = adminInput.ArticlePicture,
                CreatedOn = DateTime.UtcNow
            };

            await unitOfWork.ArticleRepository.AddArticleAsync(article);
            await unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
