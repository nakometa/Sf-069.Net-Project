using AutoMapper;
using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsHub.Api.Controllers;
using SportsHub.Api.Mapping.Models;
using SportsHub.Api.Validations;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using UnitTests.MockData;
using UnitTests.Utils;
using Xunit;

namespace UnitTests.Controllers
{
    public class ArticleControllerTests
    {
        private readonly Mock<IArticleService> _articleService;
        private readonly Mock<IGenerateModelStateDictionary> _generateModelStateDictionary;
        private readonly ArticleController _articleController;
        private readonly ArticleValidation _articleValidation;
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
            _generateModelStateDictionary = new Mock<IGenerateModelStateDictionary>();
            _articleValidation = new ArticleValidation();
            _articleController = new ArticleController(_articleService.Object, _mapper, _articleValidation, _generateModelStateDictionary.Object);
        }

        [Fact]
        public async Task GetArticleByTitleAsync_ArticleWithProvidedTitleExists_ReturnsOkStatus()
        {
            //Arrange
            string title = "testArticle";
            var article = ArticleMockData.GetArticle();
            _articleService.Setup(service => service.GetByTitleAsync(title)).ReturnsAsync(article);

            //Act
            var result = await _articleController.GetArticleByTitleAsync(title);
            var resultObject = TestHelper.GetObjectResultContent<ArticleResponseDTO>(result);
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(title, resultObject.Title);
        }

        [Fact]
        public async Task GetArticleByTitleAsync_ArticleWithProvidedTitleDoesNotExist_ReturnsBadRequest()
        {
            //Arrange
            string title = "randomTitle";
            var article = ArticleMockData.GetArticle();
            _articleService.Setup(service => service.GetByTitleAsync(title)).ReturnsAsync((Article?)null);

            //Act
            var result = await _articleController.GetArticleByTitleAsync(title);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAllAsync_ArticlesExist_ReturnsOkStatus()
        {
            //Arrange
            var articles = ArticleMockData.GetAll();
            _articleService.Setup(service => service.GetAllAsync()).ReturnsAsync(articles);

            //Act
            var result = await _articleController.GetAllAsync();
            var resultObject = TestHelper.GetObjectResultContent<IEnumerable<ArticleResponseDTO>>(result);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(3, resultObject.Count());
        }

        [Fact]
        public async Task GetAllAsync_ArticlesDontExist_ReturnsBadRequest()
        {
            //Arrange
            var articles = ArticleMockData.GetNone();
            _articleService.Setup(service => service.GetAllAsync()).ReturnsAsync(articles);

            //Act
            var result = await _articleController.GetAllAsync();

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task CreateArticleAsync_ArticleAlreadyExists_ReturnsBadRequest()
        {
            //Arrange
            var article = ArticleMockData.CreateArticle();
            _articleService.Setup(service => service.CreateArticleAsync(article)).ReturnsAsync(false);

            //Act
            var result = await _articleController.CreateArticleAsync(article);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]

        public async Task CreateArticleAsync_NewArticle_ReturnsOkStatus()
        {
            //Arrange
            var article = ArticleMockData.CreateArticle();
            _articleService.Setup(service => service.CreateArticleAsync(article)).ReturnsAsync(true);

            //Act
            var result = await _articleController.CreateArticleAsync(article);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}
