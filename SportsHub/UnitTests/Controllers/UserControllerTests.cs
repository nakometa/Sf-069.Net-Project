using System.Net;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsHub.Api.Controllers;
using SportsHub.Api.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using UnitTests.MockData;
using Xunit;

namespace UnitTests.Controllers;

public class UserControllerTests
{
    private readonly Mock<IUserService> _userService;
    private readonly UserController _userController;
    private readonly IMapper _mapper;

    public UserControllerTests()
    {
        if (_mapper == null)
        {
            var mappingConfig = new MapperConfiguration(x => x.CreateMap<User, UserResponseDTO>());
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }
        _userService = new Mock<IUserService>();
        _userController = new UserController(_userService.Object, _mapper);
    }

    [Theory]
    [InlineData("gogo")]
    public async Task GetByUserNameAsync_UserWithProvidedUsernameExists_ReturnsOkStatus(string username)
    {
        //Arrange
        var user = UserMockData.GetUser();
        _userService.Setup(service => service.GetByUsernameAsync(username)).ReturnsAsync(user);

        //Act
        var result = await _userController.GetUserByUsernameAsync(username) as ObjectResult;

        //Assert
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
    }

    [Theory]
    [InlineData("niki")]
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
    public void AdminsEndPoint_Returning_OkStatus()
    {
        //Arrange
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        _userController.ControllerContext = new ControllerContext();
        _userController.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };

        //Act
        var result = _userController.AdminsEndpoint() as ObjectResult;

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }
}