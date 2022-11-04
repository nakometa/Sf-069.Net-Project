using Microsoft.EntityFrameworkCore;
using SportsHub.DAL.Data;
using SportsHub.DAL.Repository;
using SportsHub.Domain.Models;
using UnitTests.MockData;
using Xunit;

namespace UnitTests.DataLayerTests;

public class GenericRepositoryTests
{
    private readonly ApplicationDbContext _db;
    private readonly GenericRepository<User> _repository;

    public GenericRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _db = new ApplicationDbContext(options);
        _db.Database.EnsureCreated();
        _db.Users.AddRange(UserMockData.GetUsers());
        _db.SaveChanges();
        _repository = new GenericRepository<User>(_db);
    }

    [Fact]
    public void Add_WithGivenEntity_ShouldAddItInDb()
    {
        //Arrange
        var user = UserMockData.GetUser();

        //Act
        _repository.Add(user);
        _db.SaveChanges();
        var result = _repository.GetAll();

        //Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void Delete_ShouldDeleteEntity()
    {
        //Arrange
        var user = UserMockData.GetUser();
        _repository.Add(user);
        _db.SaveChanges();
        
        //Act
        _repository.Delete(user);
        _db.SaveChanges();
        var result = _repository.GetAll();
        
        //Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void GetById_WithCorrectId_ReturnUser()
    {
        //Arrange
        var user = UserMockData.GetUser();
        int id = user.Id;
        _repository.Add(user);
        _db.SaveChanges();
        
        //Act
        var result = _repository.GetById(id);
        
        //Assert
        Assert.Equal(user, result);
    }

    [Fact]
    public void GetAll_ShouldReturnAll()
    {
        //Act
        var result = _repository.GetAll();
        
        //Assert
        Assert.Equal(2, result.Count());
    }
}