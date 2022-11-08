using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsHub.Api.Controllers;
using SportsHub.Api.Mapping.Models;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
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
            var resultObject = GetObjectResultContent<ArticleResponseDTO>(result);
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(title, resultObject.Title);
        }

        [Theory]
        [InlineData("randomArticle")]
        public async Task GetArticleByTitleAsync_ArticleWithProvidedTitleDoesNotExist_ReturnsBadRequest(string title)
        {
            //Arrange
            var article = ArticleMockData.GetArticle();
            _articleService.Setup(service => service.GetByTitleAsync(title)).ReturnsAsync((Article?)null);

            //Act
            var result = await _articleController.GetArticleByTitleAsync(title);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Theory]
        [InlineData(3)]
        public async Task GetAllAsync_ArticlesExist_ReturnsOkStatus(int count)
        {
            //Arrange
            var articles = ArticleMockData.GetAll();
            _articleService.Setup(service => service.GetAllAsync()).ReturnsAsync(articles);

            //Act
            var result = await _articleController.GetAllAsync();
            var resultObject = GetObjectResultContent<IEnumerable<ArticleResponseDTO>>(result);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(count, resultObject.Count());
        }

        [Theory]
        [InlineData(3)]
        public async Task GetAllAsync_ArticlesDontExist_ReturnsBadRequest(int count)
        {
            //Arrange
            var articles = ArticleMockData.GetNone();
            _articleService.Setup(service => service.GetAllAsync()).ReturnsAsync(articles);

            //Act
            var result = await _articleController.GetAllAsync();

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}
