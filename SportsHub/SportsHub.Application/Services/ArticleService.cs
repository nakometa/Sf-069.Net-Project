using SportsHub.Domain.Models;
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

        public async Task<bool> DeleteArticleAsync(string title)
        {
            var articleExists = await GetByTitleAsync(title) != null;
            if (!articleExists) return false;
            
            await _unitOfWork.ArticleRepository.DeleteArticleAsync(title);
            await _unitOfWork.SaveChangesAsync();
            return true;

        }
    }
}
