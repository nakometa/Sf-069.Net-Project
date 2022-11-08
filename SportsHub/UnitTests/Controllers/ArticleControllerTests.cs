using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsHub.Api.Controllers;
using SportsHub.Api.Mapping.Models;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using System.Net;
using UnitTests.MockData;
using Xunit;

namespace UnitTests.Controllers
{
    public class ArticleControllerTests
    {
        private readonly Mock<IArticleService> _articleService;
        private readonly ArticleController _articleController;
        private readonly IMapper _mapper;

        public ArticleControllerTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(x => x.CreateMap<Article, ArticleResponseDTO>());
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _articleService = new Mock<IArticleService>();
            _articleController = new ArticleController(_articleService.Object, _mapper);
        }

        [Theory]
        [InlineData("testArticle")]
        public async Task GetArticleByTitleAsync_ArticleWithProvidedTitleExists_ReturnsOkStatus(string title)
        {
            //Arrange
            var article = ArticleMockData.GetArticle();
            _articleService.Setup(service => service.GetByTitleAsync(title)).ReturnsAsync(article);

            //Act
            var result = await _articleController.GetArticleByTitleAsync(title);

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(title, result?.Value?.Title);
        }
    }
}
