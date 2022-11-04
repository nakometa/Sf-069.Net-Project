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
    private readonly GenericRepository<User> _repo;

    public GenericRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _db = new ApplicationDbContext(options);
        _db.Database.EnsureCreated();
        _db.Users.AddRange(UserMockData.GetUsers());
        _db.SaveChanges();
        _repo = new GenericRepository<User>(_db);
    }

    [Fact]
    public void Add_WithGivenEntity_ShouldAddItInDb()
    {
        //Arrange
        var user = UserMockData.GetUser();

        //Act
        _repo.Add(user);
        _db.SaveChanges();
        var result = _repo.GetAll();

        //Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void Delete_ShouldDeleteEntity()
    {
        //Arrange
        var user = UserMockData.GetUser();
        _repo.Add(user);
        _db.SaveChanges();
        
        //Act
        _repo.Delete(user);
        _db.SaveChanges();
        var result = _repo.GetAll();
        
        //Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void GetById_WithCorrectId_ReturnUser()
    {
        //Arrange
        var user = UserMockData.GetUser();
        int id = user.Id;
        _repo.Add(user);
        _db.SaveChanges();
        
        //Act
        var result = _repo.GetById(id);
        
        //Assert
        Assert.Equal(user, result);
    }

    [Fact]
    public void GetAll_ShouldReturnAll()
    {
        //Act
        var result = _repo.GetAll();
        
        //Assert
        Assert.Equal(2, result.Count());
    }
}