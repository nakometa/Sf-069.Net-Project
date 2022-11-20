using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsHub.Api.Controllers;
using SportsHub.Api.Mapping;
using SportsHub.Api.Mapping.Models;
using SportsHub.Api.Validations;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using Xunit;

namespace UnitTests.Controllers
{
    public class ArticleControllerTests
    {
        private readonly Mock<IArticleService> _articleService;
        private readonly IValidator<CreateArticleDTO> _validator;
        private readonly Mock<IGenerateModelStateDictionary> _dictionary;
        private readonly ArticleController _articleController;
        private readonly IMapper _mapper;
        private IFixture _fixture;
    
        public ArticleControllerTests()
        {
            SetupFixture();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfiles(new List<Profile>()
            {
                new ArticleMapping(),
            })).CreateMapper();
            _articleService = _fixture.Freeze<Mock<IArticleService>>();
            _validator = new ArticleValidation();
            _dictionary =_fixture.Freeze<Mock<IGenerateModelStateDictionary>>();
           _articleController = new ArticleController(_articleService.Object, _mapper,_validator, _dictionary.Object);
        }
    
        [Theory]
        [AutoData]
        public async Task GetArticleByTitleAsync_ArticleWithProvidedTitleExists_ReturnsOkStatus(string title)
        {
            //Arrange
            var article = _fixture.Build<Article>().With(x => x.Title, title).Create();
            _articleService.Setup(service => service.GetByTitleAsync(title)).ReturnsAsync(article);
    
            //Act
            var result = GetObjectResultContent( await _articleController.GetArticleByTitleAsync(title));
            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(title, result.Title);
        }
    
        [Fact]
        public async Task GetArticleByTitleAsync_ArticleWithProvidedTitleDoesNotExist_ReturnsBadRequest()
        {
            //Arrange
            string title = _fixture.Create<string>();
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
            var articles = _fixture.Build<Article>().CreateMany(3);
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
            var articles = new List<Article>();
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
            var article = _fixture.Build<CreateArticleDTO>().Create();
            _articleService.Setup(service => service.CreateArticleAsync(article)).ReturnsAsync(false);

            //Act
            var result = await _articleController.CreateArticleAsync(article);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]

        public async Task CreateArticleAsync_NewValidArticle_ReturnsOkStatus()
        {
            //Arrange
            var article = _fixture.Build<CreateArticleDTO>().Create();
            _articleService.Setup(service => service.CreateArticleAsync(article)).ReturnsAsync(true);

            //Act
            var result = await _articleController.CreateArticleAsync(article);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task EditArticleAsync_NonexistentArticle_ReturnsBadRequest()
        {
            //Arrange
            var article = _fixture.Build<CreateArticleDTO>().Create();
            _articleService.Setup(service => service.EditArticleAsync(article)).ReturnsAsync(false);

            //Act
            var result = await _articleController.EditArticleAsync(article);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task EditArticleAsync_ValidInput_ReturnsOkStatus()
        {
            //Arrange
            var article = _fixture.Build<CreateArticleDTO>().Create();
            _articleService.Setup(service => service.EditArticleAsync(article)).ReturnsAsync(true);

            //Act
            var result = await _articleController.EditArticleAsync(article);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
        
        private void SetupFixture()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}

