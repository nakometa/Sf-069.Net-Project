using SportsHub.Api.Exceptions.CustomExceptionModels;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;
using SportsHub.Domain.Models.Constants;
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

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await _unitOfWork.ArticleRepository.GetAllAsync();
        }

        public async Task<Article?> GetByTitleAsync(string title)
        {
            return await _unitOfWork.ArticleRepository.GetByTitleAsync(title) ??
                   throw new NotFoundException( string.Format(ExceptionMessages.NotFound, ExceptionMessages.Article));
        }

        public async Task<bool> CreateArticleAsync(CreateArticleDTO adminInput)
        {
            var articleExists = await GetByTitleAsync(adminInput.Title) != null;

            if (articleExists)
            {
                return false;
            }

            var categoryExists = GetCategoryById(adminInput.CategoryId);

            if(categoryExists == null)
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

        private async Task<Category?> GetCategoryById(int categoryId)
        {
            return await _unitOfWork.CategoryRepository.GetCategoryById(categoryId);
        }
        
        public async Task<List<Article>> GetListOfArticlesBySubstringAsync(string substring)
        {
            return await _unitOfWork.ArticleRepository.GetBySubstringAsync(substring);
        }
    }
}
