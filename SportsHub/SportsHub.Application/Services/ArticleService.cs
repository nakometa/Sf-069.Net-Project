using SportsHub.Api.Exceptions.CustomExceptionModels;
using SportsHub.Domain.Models;
using SportsHub.Domain.Models.Constants;
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
            return await _unitOfWork.ArticleRepository.GetByTitleAsync(title) ??
                   throw new NotFoundException( string.Format(ExceptionMessages.NotFound, ExceptionMessages.Article));
        }
    }
}
