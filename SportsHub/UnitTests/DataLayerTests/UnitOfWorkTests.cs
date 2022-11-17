using AutoFixture;
using Microsoft.EntityFrameworkCore;
using SportsHub.DAL.Data;
using SportsHub.DAL.UOW;
using SportsHub.Domain.Models;
using SportsHub.Domain.UOW;
using Xunit;

namespace UnitTests.DataLayerTests;

public class UnitOfWorkTests : IDisposable
{
    private ApplicationDbContext _db;
    private IUnitOfWork _unitOfWork;
    private IFixture _fixture;

    public UnitOfWorkTests()
    {
        SetupFixture();
        SetupDb();
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
    
    private void SetupDb()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _db = new ApplicationDbContext(options);
        _db.Database.EnsureCreated();
        _db.Users.AddRange(_fixture.Build<User>().CreateMany(2));
        _unitOfWork = new UnitOfWork(_db);
    }

    private void SetupFixture()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }
}