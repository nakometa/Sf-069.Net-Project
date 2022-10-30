using Microsoft.EntityFrameworkCore;
using SportsHub.AppService.Services;
using SportsHub.DAL.Data;
using SportsHub.DAL.UOW;
using SportsHub.Domain.UOW;
using Xunit;

namespace UnitTests.Services;

public class UserServiceTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        _context.Users.AddRange(MockData.UserMockData.GetUsers());
        _unitOfWork = new UnitOfWork(_context);
        _unitOfWork.SaveChangesAsync();
    }

    [Theory]
    [InlineData("firstuser@mail.com")]
    [InlineData("seconduser@mail.com")]
    public async Task GetByEmailAsync_WithCorrectEmail_ReturnsCorrectUser(string email)
    {
        //Arrange
        var sut = new UserService(_unitOfWork);

        //Act
        var result = await sut.GetByEmailAsync(email);

        //Assert
        Assert.Equal(email, result.Email);
    }

    [Theory]
    [InlineData("email@email.com")]
    [InlineData(null)]
    public async Task GetByEmailAsync_WithIncorrectEmail_ReturnsNull(string email)
    {
        //Arrange
        var sut = new UserService(_unitOfWork);

        //Act
        var result = await sut.GetByEmailAsync(email);

        //Assert
        Assert.Null(result);
    }

    [Theory]
    [InlineData("gogo")]
    [InlineData("niki")]
    public async Task GetByUsernameAsync_WithCorrectUsername_ReturnsCorrectUser(string userName)
    {
        //Arrange
        var sut = new UserService(_unitOfWork);

        //Act
        var result = await sut.GetByUsernameAsync(userName);

        //Assert
        Assert.Equal(userName, result.Username);
    }

    [Theory]
    [InlineData("peshkata")]
    public async Task GetByUsernameAsync_WithIncorrectUsername_ReturnsNull(string userName)
    {
        //Arrange
        var sut = new UserService(_unitOfWork);

        //Act
        var result = await sut.GetByUsernameAsync(userName);

        //Assert
        Assert.Null(result);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _unitOfWork.Dispose();
    }
}