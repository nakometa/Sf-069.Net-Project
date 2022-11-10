using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsHub.Api.Controllers;
using SportsHub.Api.Mapping.Models;
using SportsHub.Api.Validations;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using UnitTests.MockData;
using Xunit;

namespace UnitTests.Controllers
{
    public class ArticleControllerTests
    {
        private readonly Mock<IArticleService> _articleService;
        private readonly Mock<IValidator<CreateArticleDTO>> _articleValidator;
        private readonly Mock<IGenerateModelStateDictionary> _generateModelStateDictionary;
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
            _articleValidator = new Mock<IValidator<CreateArticleDTO>>();
            _generateModelStateDictionary = new Mock<IGenerateModelStateDictionary>();
            _articleController = new ArticleController(_articleService.Object, _mapper, _articleValidator.Object, _generateModelStateDictionary.Object);
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
            var resultObject = GetObjectResultContent<ArticleResponseDTO>(result);
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
            var resultObject = GetObjectResultContent<IEnumerable<ArticleResponseDTO>>(result);

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

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}
