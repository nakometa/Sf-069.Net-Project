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

        public async Task<bool> DeleteArticleAsync(int id)
        {
            var articleExists = await _unitOfWork.ArticleRepository.DeleteArticleAsync(id);
            
            //after merging exception handling we can throw exception insted of returning again false
            if (!articleExists) return false;
            
            await _unitOfWork.SaveChangesAsync();
            
            return true;
        }
    }
}
