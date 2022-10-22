using Microsoft.EntityFrameworkCore;
using SportsHub.AppService.Services;
using SportsHub.DAL.Data;
using SportsHub.DAL.UOW;
using SportsHub.Domain.UOW;
using Xunit;

namespace UnitTests.Services;

public class TestUserService : IDisposable
{
    private readonly ApplicationDbContext _context;
    private IUnitOfWork _unitOfWork;

    public TestUserService()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        _context.Users.AddRange(MockData.UsersMockData.GetUsers());
        _unitOfWork = new UnitOfWork(_context);
        _unitOfWork.SaveChangesAsync();
    }

    [Theory]
    [InlineData("firstuser@mail.com")]
    [InlineData("seconduser@mail.com")]
    public async Task GetByEmailAsync_Should_Work_Properly(string email)
    {
        var sut = new UserService(_unitOfWork);

        var result = await sut.GetByEmailAsync(email);

        Assert.Equal(email, result.Email);
    }

    [Theory]
    [InlineData("email@email.com")]
    [InlineData(null)]
    public async Task GetByEmailAsync_Should_Return_Null(string email)
    {
        var sut = new UserService(_unitOfWork);

        var result = await sut.GetByEmailAsync(email);

        Assert.Null(result);
    }

    [Theory]
    [InlineData("gogo")]
    [InlineData("niki")]
    public async Task GetByUsernameAsync_Should_Work_Properly(string userName)
    {
        var sut = new UserService(_unitOfWork);

        var result = await sut.GetByUsernameAsync(userName);

        Assert.Equal(userName, result.Username);
    }

    [Theory]
    [InlineData("peshkata")]
    public async Task GetByUsernameAsync_Should_Return_Null(string userName)
    {
        var sut = new UserService(_unitOfWork);

        var result = await sut.GetByUsernameAsync(userName);

        Assert.Null(result);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _unitOfWork.Dispose();
    }
}