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

public class TestUserController
{
    private readonly Mock<IUserService> _userService;
    private readonly UserController _userController;
    private readonly IMapper _mapper;

    public TestUserController()
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

    [Fact]
    public async Task GetByUserNamelAsync_Should_Work_Properly()
    {
        var user = UserMockData.GetUser();
        _userService.Setup(service => service.GetByUsernameAsync("gogo")).Returns(Task.FromResult<User?>(user));

        var result = await _userController.GetUserByUsernameAsync("gogo") as ObjectResult;

        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
    }

    [Fact]
    public async Task GetByuserNameAsync_ShouldReturn_BadRequest()
    {
        _userService.Setup(service => service.GetByUsernameAsync("gogo")).Returns(Task.FromResult<User?>(null));

        var result = await _userController.GetUserByUsernameAsync("gogo") as ObjectResult;

        Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Public_ReturnOkResult()
    {
        var result = _userController.Public() as ObjectResult;

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void AdminsEndPoint_Returning_OkStatus()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        _userController.ControllerContext = new ControllerContext();
        _userController.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };

        var result = _userController.AdminsEndpoint() as ObjectResult;

        Assert.IsType<OkObjectResult>(result);
    }
}