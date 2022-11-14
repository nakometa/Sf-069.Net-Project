using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Moq;
using SportsHub.AppService.Authentication;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Authentication.PasswordHasher;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using SportsHub.Domain.PasswordHasher;
using Xunit;

namespace UnitTests.Services;

public class AuthenticationServiceTests
{
    private readonly Mock<IUserService> _userService;
    private readonly Mock<IPasswordHasher> _passwordHasher;
    private readonly IAuthenticationService _authentication;
    private IFixture _fixture;
    private readonly IPasswordCheckResult _passwordCheckResult;

    public AuthenticationServiceTests()
    {
        SetupFixture();
        _userService = _fixture.Freeze<Mock<IUserService>>();
        _passwordHasher = _fixture.Freeze<Mock<IPasswordHasher>>();
        _authentication = new AuthenticationService(_userService.Object, _passwordHasher.Object);
        _passwordCheckResult = new PasswordCheckResult(true, false);
    }

    //Not Working !!!
    [Theory]
    [AutoData]
    public async Task Authenticate_WithUsername_ReturnUser(string userName)
    {
        //Arrange
        var givenUser = _fixture.Build<UserLoginDTO>().With(x=>x.UsernameOrEmail, userName).Create();
        var user = _fixture.Build<User>().With(x=>x.Email, userName).Create();
        _userService.Setup(service => service.GetByEmailOrUsernameAsync(userName)).ReturnsAsync(user);
        _passwordHasher.Setup(x => x.Check(user.Password, givenUser.Password)).Returns(_passwordCheckResult);
        
        //Act
        var result = await _authentication.Authenticate(givenUser);
    
        //Assert
        Assert.Equal(result.Username, user.Username);
    
    }

    [Fact]
    public async Task Authenticate_WithUsername_ReturnNull()
    {
        //Arrange
        var givenUser = _fixture.Build<UserLoginDTO>().Create();
        _userService.Setup(service => service.GetByUsernameAsync(givenUser.UsernameOrEmail)).ReturnsAsync((User?)null);

        //Act
        var result = await _authentication.Authenticate(givenUser);

        //Assert
        Assert.Null(result);
    }

    //Not Working !!!!
    [Theory]
    [AutoData]
    public async Task Authenticate_WithEmail_ReturnUser(string email)
    {
        //Arrange
        var givenUser = _fixture.Build<UserLoginDTO>().With(x=>x.UsernameOrEmail, email).Create();
        var user = _fixture.Build<User>().With(x=>x.Email, email).Create();
        _userService.Setup(service => service.GetByEmailOrUsernameAsync(email)).ReturnsAsync(user);
        _passwordHasher.Setup(x => x.Check(user.Password, givenUser.Password)).Returns(_passwordCheckResult);
    
        //Act
        var result = await _authentication.Authenticate(givenUser);
    
        //Assert
        Assert.Equal(result.Email, user.Email);
    }

    [Fact]
    public async Task Authenticate_WithEmail_ReturnNull()
    {
        //Arrange
        var givenUser = _fixture.Build<UserLoginDTO>().Create();
        _userService.Setup(service => service.GetByUsernameAsync(givenUser.UsernameOrEmail)).ReturnsAsync((User?)null);

        //Act
        var result = await _authentication.Authenticate(givenUser);

        //Assert
        Assert.Null(result);
    }
    
    private void SetupFixture()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }
}