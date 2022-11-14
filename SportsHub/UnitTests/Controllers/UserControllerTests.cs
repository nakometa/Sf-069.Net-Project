using System.Net;
using System.Security.Claims;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsHub.Api.Controllers;
using SportsHub.Api.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using Xunit;

namespace UnitTests.Controllers;

public class UserControllerTests
{
    private readonly Mock<IUserService> _userService;
    private readonly UserController _userController;
    private readonly IMapper _mapper;
    private IFixture _fixture;
    
    public UserControllerTests()
    {
        SetupFixture();
        if (_mapper == null)
        {
            var mappingConfig = new MapperConfiguration(x => x.CreateMap<User, UserResponseDTO>());
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }
        _userService = _fixture.Freeze<Mock<IUserService>>();
        _userController = new UserController(_userService.Object, _mapper);
    }
    
    [Theory]
    [AutoData]
    public async Task GetByUserNameAsync_UserWithProvidedUsernameExists_ReturnsOkStatus(string username)
    {
        //Arrange
        var user = _fixture.Build<User>().With(x => x.Username, username).Create();
        _userService.Setup(service => service.GetByUsernameAsync(username)).ReturnsAsync(user);
    
        //Act
        var result = await _userController.GetUserByUsernameAsync(username) as ObjectResult;
    
        //Assert
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
    }
    
    [Theory]
    [AutoData]
    public async Task GetByuserNameAsync_UserWithProvidedUsernameDoesNotExist_ReturnsBadRequest(string username)
    {
        //Arrange
        _userService.Setup(service => service.GetByUsernameAsync(username)).ReturnsAsync((User?)null);
    
        //Act
        var result = await _userController.GetUserByUsernameAsync(username) as ObjectResult;
    
        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public void Public_ReturnOkResult()
    {
        //Act
        var result = _userController.Public() as ObjectResult;
    
        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AdminsEndPoint_Returning_OkStatus()
    {
        //Arrange
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        _userController.ControllerContext = new ControllerContext();
        _userController.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };
    
        // This need to be looked at
        //Act
        var result = await _userController.AdminsEndpoint() as ObjectResult;
    
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