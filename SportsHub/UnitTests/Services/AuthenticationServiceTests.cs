using Moq;
using SportsHub.AppService.Authentication;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using UnitTests.MockData;
using Xunit;

namespace UnitTests.Services;

public class AuthenticationServiceTests
{
    private readonly Mock<IUserService> _userService;
    private readonly IAuthenticationService _authentication;

    public AuthenticationServiceTests()
    {
        _userService = new Mock<IUserService>();
        _authentication = new AuthenticationService(_userService.Object);
    }

    [Fact]
    public async Task Authenticate_WithUsername_ReturnUser()
    {
        //Arrange
        var givenUser = UserMockData.GetUserWithUsername();
        var user = UserMockData.GetUser();
        _userService.Setup(service => service.GetByUsernameAsync(givenUser.UsernameOrEmail)).ReturnsAsync(user);

        //Act
         var result = await _authentication.Authenticate(givenUser); 
       
         //Assert
        Assert.Equal(result.Username, user.Username );
        Assert.Equal(result.Password, user.Password);
        
    }

    [Fact]
    public async Task Authenticate_WithUsername_ReturnNull()
    {
        //Arrange
        var givenUser = UserMockData.GetUserWithUsername();
        _userService.Setup(service => service.GetByUsernameAsync(givenUser.UsernameOrEmail)).ReturnsAsync((User?)null);

        //Act
        var result = await _authentication.Authenticate(givenUser);
      
        //Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task Authenticate_WithEmail_ReturnUser()
    {
        //Arrange
        var givenUser = UserMockData.GetUserWithEmail();
        var user = UserMockData.GetUser();
        _userService.Setup(service => service.GetByEmailAsync(givenUser.UsernameOrEmail)).ReturnsAsync(user);

        //Act
        var result = await _authentication.Authenticate(givenUser); 
      
        //Assert
        Assert.Equal(result.Email, user.Email );
        Assert.Equal(result.Password, user.Password);
    }
    
    [Fact]
    public async Task Authenticate_WithEmail_ReturnNull()
    {
        //Arrange
        var givenUser = UserMockData.GetUserWithEmail();
        _userService.Setup(service => service.GetByUsernameAsync(givenUser.UsernameOrEmail)).ReturnsAsync((User?)null);

        //Act
        var result = await _authentication.Authenticate(givenUser);
      
        //Assert
        Assert.Null(result);
    }
}