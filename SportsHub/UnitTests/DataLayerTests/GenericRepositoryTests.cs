using AutoFixture;
using Microsoft.EntityFrameworkCore;
using SportsHub.DAL.Data;
using SportsHub.DAL.Repository;
using SportsHub.Domain.Models;
using Xunit;

namespace UnitTests.DataLayerTests;

public class GenericRepositoryTests
{
    private readonly ApplicationDbContext _db;
    private readonly GenericRepository<User> _repository;
    private IFixture _fixture;
    private readonly User _user;

    public GenericRepositoryTests()
    {
        SetupFixture();
        _user = _fixture.Build<User>().Create();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _db = new ApplicationDbContext(options);
        _db.Database.EnsureCreated();
        _db.Users.AddRange(_fixture.Build<User>().CreateMany(2));
        _db.SaveChanges();
        _repository = new GenericRepository<User>(_db);
    }

    [Fact]
    public void Add_WithGivenEntity_ShouldAddItInDb()
    {
        //Act
        _repository.Add(_user);
        _db.SaveChanges();
        var result = _repository.GetAll();

        //Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void Delete_ShouldDeleteEntity()
    {
        //Arrange
        _repository.Add(_user);
        _db.SaveChanges();
        
        //Act
        _repository.Delete(_user);
        _db.SaveChanges();
        var result = _repository.GetAll();
        
        //Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void GetById_WithCorrectId_ReturnUser()
    {
        //Arrange
        int id = _user.Id;
        _repository.Add(_user);
        _db.SaveChanges();
        
        //Act
        var result = _repository.GetById(id);
        
        //Assert
        Assert.Equal(_user, result);
    }

    [Fact]
    public void GetAll_ShouldReturnAll()
    {
        //Act
        var result = _repository.GetAll();
        
        //Assert
        Assert.Equal(2, result.Count());
    }
    
    private void SetupFixture()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }
}