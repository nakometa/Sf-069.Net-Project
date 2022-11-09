using Moq;
using SportsHub.AppService.Services;
using SportsHub.Domain.Repository;
using SportsHub.Domain.UOW;
using Xunit;

namespace UnitTests.Services;

public class ArticleServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IArticleRepository> _repository;
    private readonly ArticleService _service;

    public ArticleServiceTests()
    {
        _repository = new Mock<IArticleRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _unitOfWork.Setup(u => u.ArticleRepository).Returns(_repository.Object);
        _service = new ArticleService(_unitOfWork.Object);
    }

    [Theory]
    [InlineData(2)]
    public async Task DeleteArticleAsync_WithCorrectId_ShouldReturnTrue(int id)
    {
        //Arrange
        _repository.Setup(r => r.DeleteArticleAsync(id)).ReturnsAsync(true);

        //Act
        var result = await _service.DeleteArticleAsync(id);
      
        //Assert
        Assert.Equal(true, result);
    }
    
    [Theory]
    [InlineData(6)]
    public async Task DeleteArticleAsync_WithoutCorrectId_ShouldReturnFalse(int id)
    {
        //Arrange
        _repository.Setup(r => r.DeleteArticleAsync(id)).ReturnsAsync(false);

        //Act
        var result = await _service.DeleteArticleAsync(id);
      
        //Assert
        Assert.Equal(false, result);
    }
}