using SportsHub.Domain.Models;
using SportsHub.Domain.UOW;

namespace SportsHub.AppService.Services
{
    public class FilterService : IFilterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FilterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Article>> GetListOfArticlesBySubstringAsync(string substring)
        {
            return await _unitOfWork.ArticleRepository.GetBySubstringAsync(substring);
        }
    }
}
