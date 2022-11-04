using Microsoft.EntityFrameworkCore;
using Moq;
using SportsHub.DAL.Data;
using SportsHub.DAL.Repository;
using SportsHub.DAL.UOW;
using SportsHub.Domain.UOW;
using UnitTests.MockData;
using Xunit;

namespace UnitTests.DataLayerTests;

public class UnitOfWorkTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _db = new ApplicationDbContext(options);
        _db.Database.EnsureCreated();
        _db.Users.AddRange(UserMockData.GetUsers());
        _unitOfWork = new UnitOfWork(_db);
    }

    [Fact]
    public void SaveChangesAsync_WithGivenDb_ShouldSaveCorrectly()
    {
        //Arrange
        _unitOfWork.SaveChangesAsync();

        //Act
        var result = _unitOfWork.UserRepository.GetAll();

        //Assert
        Assert.Equal(result.Count(), 2);
    }
    
    public void Dispose()
    {
        _db.Database.EnsureDeleted();
        _unitOfWork.Dispose();
    }
}