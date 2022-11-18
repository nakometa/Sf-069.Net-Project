using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsHub.Api.Controllers;
using SportsHub.Api.Mapping.Models;
using SportsHub.Api.Validations;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using UnitTests.MockData;
using UnitTests.Utils;
using Xunit;

namespace UnitTests.Controllers
{
    //!!ActicleController need to implement IGenerateModelStateDictionary to work . Currently all of this tests dont work because of this!!
    public class ArticleControllerTests
    {
        private readonly Mock<IArticleService> _articleService;
        private readonly Mock<IGenerateModelStateDictionary> _generateModelStateDictionary;
        private readonly ArticleController _articleController;
        private readonly ArticleValidation _articleValidation;
        private readonly IMapper _mapper;
        private IFixture _fixture;

        public ArticleControllerTests()
        {
            SetupFixture();

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

        [Theory]
        [AutoData]
        public async Task GetArticleByTitleAsync_ArticleWithProvidedTitleExists_ReturnsOkStatus(string title)
        {
            //Arrange
            var article = _fixture.Build<Article>().With(x => x.Title, title).Create();
            _articleService.Setup(service => service.GetByTitleAsync(title)).ReturnsAsync(article);

            //Act
            var result = await _articleController.GetArticleByTitleAsync(title);
            var resultObject = TestHelper.GetObjectResultContent<ArticleResponseDTO>(result);

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
            var resultObject = TestHelper.GetObjectResultContent<IEnumerable<ArticleResponseDTO>>(result);

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
            var article = ArticleMockData.CreateArticle();
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
            var article = ArticleMockData.CreateArticle();
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
            var article = ArticleMockData.CreateArticle();
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
            var article = ArticleMockData.CreateArticle();
            _articleService.Setup(service => service.EditArticleAsync(article)).ReturnsAsync(true);

            //Act
            var result = await _articleController.EditArticleAsync(article);

            //Assert
            Assert.IsType<OkObjectResult>(result);
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
