using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Moq;
using SportsHub.Api.Exceptions.CustomExceptionModels;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using SportsHub.Domain.Models.Constants;
using SportsHub.Domain.Repository;
using SportsHub.Domain.UOW;
using Xunit;

namespace UnitTests.Services;

public class UserServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserRepository> _repo;
    private User _user;
    private readonly UserService _userService;
    private IFixture _fixture;

    public UserServiceTests()
    {
        SetupFixture();
        _unitOfWorkMock = _fixture.Freeze<Mock<IUnitOfWork>>();
        _repo = _fixture.Freeze<Mock<IUserRepository>>();
        _unitOfWorkMock.Setup(u => u.UserRepository).Returns(_repo.Object);
        _userService = new UserService(_unitOfWorkMock.Object);
    }
    
    [Theory]
    [AutoData]
    public async Task GetByEmailAsync_WithCorrectEmail_ReturnsCorrectUser(string email)
    {
        //Arrange
        _user = _fixture.Build<User>().With(x => x.Email, email).Create();
        _repo.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync(_user);

        //Act
        var result = await _userService.GetByEmailAsync(email);

        //Assert
        Assert.Equal(email, result.Email);
    }

    [Theory]
    [AutoData]
    public async Task GetByEmailAsync_WithIncorrectEmail_ReturnsNull(string email)
    {
        //Arrange
        _repo.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync((User?)null);

        //Act
         var exception =Assert.ThrowsAsync<NotFoundException>(() => _userService.GetByEmailAsync(email));

        //Assert
        Assert.Equal(exception.Result.ErrorCode, StatusCodeConstants.NotFound);
        Assert.Equal(exception.Result.Message, string.Format(ExceptionMessages.NotFound,ExceptionMessages.User));
    }

    [Theory]
    [AutoData]
    public async Task GetByUsernameAsync_WithCorrectUsername_ReturnsCorrectUser(string userName)
    {
        //Arrange
        _user = _fixture.Build<User>().With(x => x.Username, userName).Create();
        _repo.Setup(r => r.GetByUsernameAsync(userName)).ReturnsAsync(_user);

        //Act
        var result = await _userService.GetByUsernameAsync(userName);

        //Assert
        Assert.Equal(userName, result.Username);
    }

    [Theory]
    [AutoData]
    public async Task GetByUsernameAsync_WithIncorrectUsername_ReturnsNull(string userName)
    {
        //Arrange
        _repo.Setup(r => r.GetByUsernameAsync(userName)).ReturnsAsync((User?)null);
    
        //Act
        var exception =Assert.ThrowsAsync<NotFoundException>(() => _userService.GetByUsernameAsync(userName));

        //Assert
        Assert.Equal(exception.Result.ErrorCode, StatusCodeConstants.NotFound);
        Assert.Equal(exception.Result.Message, string.Format(ExceptionMessages.NotFound,ExceptionMessages.User));
    }

    private void SetupFixture()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }
}