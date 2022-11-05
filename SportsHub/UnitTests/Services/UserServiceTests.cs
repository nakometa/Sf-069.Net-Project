using Moq;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using SportsHub.Domain.Repository;
using SportsHub.Domain.UOW;
using UnitTests.MockData;
using Xunit;

namespace UnitTests.Services;

public class UserServiceTests 
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserRepository> _repo;
    private readonly User _user;
    private readonly UserService _userService;
    
    public UserServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _repo = new Mock<IUserRepository>();
        _unitOfWorkMock.Setup(u => u.UserRepository).Returns(_repo.Object);
        _user = UserMockData.GetUser();
        _userService = new UserService(_unitOfWorkMock.Object);
    }

    [Theory]
    [InlineData("goshko88@mail.bg")]
    public async Task GetByEmailAsync_WithCorrectEmail_ReturnsCorrectUser(string email)
    {
        //Arrange
        _repo.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync(_user);

        //Act
        var result = await _userService.GetByEmailAsync(email);

        //Assert
        Assert.Equal(email, result.Email);
    }

    [Theory]
    [InlineData("email@email.com")]
    [InlineData(null)]
    public async Task GetByEmailAsync_WithIncorrectEmail_ReturnsNull(string email)
    {
        //Arrange
        _repo.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync((User?)null);
    
        //Act
        var result = await _userService.GetByEmailAsync(email);
    
        //Assert
        Assert.Null(result);
    }
    
    [Theory]
    [InlineData("gogo")]
    public async Task GetByUsernameAsync_WithCorrectUsername_ReturnsCorrectUser(string userName)
    {
        //Arrange
        _repo.Setup(r => r.GetByUsernameAsync(userName)).ReturnsAsync(_user);
    
        //Act
        var result = await _userService.GetByUsernameAsync(userName);
    
        //Assert
        Assert.Equal(userName, result.Username);
    }
    
    [Theory]
    [InlineData("peshkata")]
    public async Task GetByUsernameAsync_WithIncorrectUsername_ReturnsNull(string userName)
    {
        //Arrange
        _repo.Setup(r => r.GetByUsernameAsync(userName)).ReturnsAsync((User?)null);
    
        //Act
        var result = await _userService.GetByUsernameAsync(userName);
    
        //Assert
        Assert.Null(result);
    }
}