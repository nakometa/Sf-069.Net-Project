using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using SportsHub.Api.Exceptions.CustomExceptionModels;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.Constants;
using SportsHub.Domain.Models;
using SportsHub.Domain.Repository;
using SportsHub.Domain.UOW;
using Xunit;

namespace DemoTests.AppServiceTests
{
    public class ArticleServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IArticleRepository> _mockArticleRepo;
        private readonly Mock<IMapper> _mockAutoMapper;
        private readonly ArticleService _sut;

        public ArticleServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockArticleRepo = new Mock<IArticleRepository>();
            _mockAutoMapper = new Mock<IMapper>();
            _sut = new ArticleService(_mockUnitOfWork.Object, _mockAutoMapper.Object);

            _mockUnitOfWork.Setup(x => x.ArticleRepository).Returns(_mockArticleRepo.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyCollection_WhenNoArticles()
        {
            //Act
            var result = await _sut.GetAllAsync();

            //Assert
            Assert.Empty(result);
            _mockArticleRepo.Verify(x => x.GetAllAsync(), Times.Once());
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnArticles_WhenTheyExist()
        {
            //Arrange
            var articlesList = new List<Article>()
            {
                new Article{ Title = "Test1" },
                new Article{ Title = "Test2" }
            };

            _mockArticleRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(articlesList);

            //Act
            var result = await _sut.GetAllAsync();

            //Assert
            Assert.Collection(result,
                item => Assert.Equal(item.Title, "Test1"),
                item => Assert.Equal(item.Title, "Test2"));

            _mockArticleRepo.Verify(x => x.GetAllAsync(), Times.Once());
        }

        [Theory]
        [InlineAutoData("Test", 1)]
        [InlineAutoData("Test123", 2)]
        public async Task CreateArticleAsync_ShouldWork_WhenValid(string test, int categoryId)
        {
            //Arrange
            Category category = new Category { };
            _mockUnitOfWork.Setup(x => x.CategoryRepository.GetCategoryById(It.IsAny<int>())).ReturnsAsync(category);
            var createArticleDto = new CreateArticleDTO { Title = test, CategoryId = categoryId };

            //Act
            var result = _sut.CreateArticleAsync(createArticleDto).Result;

            //Assert
            Assert.Equal(result, ValidationMessages.ArticleCreatedSuccessfully);
            _mockArticleRepo.Verify(x => x.AddArticleAsync(It.IsAny<Article>()), Times.Once());
            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Theory]
        [InlineAutoData("Test")]
        [InlineAutoData("Test123")]
        public async Task CreateArticleAsync_ShouldThrow_WhenArticleTitleExists(string title)
        {
            //Arrange
            var article = new Article { Title = title };
            _mockArticleRepo.Setup(x => x.GetByTitleAsync(title)).ReturnsAsync(article);

            var createArticleDto = new CreateArticleDTO
            {
                Title = title,
            };

            //Assert
            Assert.ThrowsAsync<BusinessLogicException>(() => _sut.CreateArticleAsync(createArticleDto));
            _mockArticleRepo.Verify(x => x.GetByTitleAsync(title), Times.Once());
        }

        [Theory]
        [InlineAutoData("Test", 1)]
        [InlineAutoData("Test123", 2)]
        public async Task CreateArticleAsync_ShouldThrow_WhenCategoryIdIsInvalid(string test, int categoryId)
        {
            //Arrange
            Category category = null;
            _mockUnitOfWork.Setup(x => x.CategoryRepository.GetCategoryById(It.IsAny<int>())).ReturnsAsync(category);
            var createArticleDto = new CreateArticleDTO { Title = test , CategoryId = categoryId };

            //Assert
            Assert.ThrowsAsync<BusinessLogicException>(() => _sut.CreateArticleAsync(createArticleDto));
        }

    }
}
