using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using SportsHub.Api.Exceptions.CustomExceptionModels;
using SportsHub.Api.Mapping;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using SportsHub.Domain.Models.Constants;
using SportsHub.Domain.Repository;
using SportsHub.Domain.UOW;
using Xunit;

namespace UnitTests.Services;

public class ArticleServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IArticleRepository> _repository;
    private readonly ArticleService _service;
    private readonly IMapper _mapper;
    private IFixture _fixture;

    public ArticleServiceTests()
    {
        SetupFixture();
        _repository = _fixture.Freeze<Mock<IArticleRepository>>();
        _unitOfWork = _fixture.Freeze<Mock<IUnitOfWork>>();
        _unitOfWork.Setup(u => u.ArticleRepository).Returns(_repository.Object);
        _mapper = new MapperConfiguration(cfg => cfg.AddProfiles(new List<Profile>()
        {
            new ArticleMapping()
        })).CreateMapper();
        _service = new ArticleService(_unitOfWork.Object, _mapper);
    }

    [Fact]
    public async Task GetAllAsync_ReturnAllArticles()
    {
        //Arrange
        IEnumerable<Article> articles = _fixture.Build<Article>().CreateMany(4);
        _unitOfWork.Setup(x => x.ArticleRepository.GetAllAsync()).ReturnsAsync(articles);

        //Act
        var result = await _service.GetAllAsync();

        //Assert
        Assert.Equal(articles.Count(), result.Count());
    }

    [Theory]
    [AutoData]
    public async Task GetListOfArticlesBySubstringAsync_WithSubstringMatchingTitle_ReturnsArticles(string substring)
    {
        //Arrange
        var articles = _fixture.Build<Article>().CreateMany(3).ToList();
        _unitOfWork.Setup(x => x.ArticleRepository.GetBySubstringAsync(substring)).ReturnsAsync(articles);

        //Act
        var result = await _service.GetListOfArticlesBySubstringAsync(substring);

        //Assert
        Assert.Equal(articles.Count(), result.Count);
    }

    [Theory]
    [AutoData]
    public async Task GetListOfArticlesBySubstringAsync_WithSubstringMatchingAuthorUsername_ReturnsArticles(string substring)
    {
        //Arrange
        var articles = _fixture.Build<Article>().CreateMany(4).ToList();
        _unitOfWork.Setup(x => x.ArticleRepository.GetBySubstringAsync(substring)).ReturnsAsync(articles);
        
        //Act
        var result = await _service.GetListOfArticlesBySubstringAsync(substring);

        //Assert
        Assert.Equal(articles.Count(), result.Count);
        
    }

    [Theory]
    [AutoData]
    public async Task DeleteArticleAsync_WithoutCorrectId_ShouldThrowException(int id)
    {
        //Arrange
        _unitOfWork.Setup(x => x.ArticleRepository.GetByIdAsync(id)).ReturnsAsync((Article?)null);
        
        //Act
        var exception = Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteArticleAsync(id));

        //Assert
        Assert.Equal(exception.Result.ErrorCode, StatusCodeConstants.NotFound);
        Assert.Equal(exception.Result.Message, string.Format(ExceptionMessages.NotFound,ExceptionMessages.Article));
    }

    [Theory]
    [AutoData]
    public async Task GetByTitleAsync_WithExistingTitle_ReturnsArticle(string title)
    {
        //Arrange
        var article = _fixture.Build<Article>().With(x => x.Title, title).Create();
        _unitOfWork.Setup(x => x.ArticleRepository.GetByTitleAsync(title)).ReturnsAsync(article);

        //Act
        var result = await _service.GetByTitleAsync(title);

        //Assert
        Assert.Equal(result.Title, article.Title);
    }

    [Theory]
    [AutoData]
    public async Task GetByTitleAsync_WithWrongTitle_ReturnsException(string title)
    {
        //Arrange
        _unitOfWork.Setup(x => x.ArticleRepository.GetByTitleAsync(title)).ReturnsAsync((Article?)null);

        //Act
        var exception = Assert.ThrowsAsync<NotFoundException>(() => _service.GetByTitleAsync(title));

        //Assert
        Assert.Equal(exception.Result.ErrorCode, StatusCodeConstants.NotFound);
        Assert.Equal(exception.Result.Message, string.Format(ExceptionMessages.NotFound, ExceptionMessages.Article));
    }

    private void SetupFixture()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }
}