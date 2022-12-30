using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Authorization;
using Moq;
using SportsHub.Api.Exceptions.CustomExceptionModels;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using SportsHub.Domain.Models.Constants;
using SportsHub.Domain.Repository;
using SportsHub.Domain.UOW;
using System.Security.Claims;
using Xunit;

namespace DemoTests.AppServiceTests
{

    public class UserServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly UserService _sut;

        public UserServiceTests()
        {
            _mockUnitOfWork= new Mock<IUnitOfWork>();
            _mockUserRepo= new Mock<IUserRepository>();
            _sut = new UserService(_mockUnitOfWork.Object);
            _mockUnitOfWork.Setup(x => x.UserRepository).Returns(_mockUserRepo.Object);
        }

        [Theory]
        [InlineAutoData("Test")]
        [InlineAutoData("Test123")]
        public async Task GetByUsernameAsync_ShouldReturnUser_WhenUsernameExists(string username)
        {
            //Arrange
            var user = new User()
            {
                Username = username
            };

            _mockUserRepo.Setup(x => x.GetByUsernameAsync(username)).ReturnsAsync(user);

            //Act
            var result = await _sut.GetByUsernameAsync(username);

            //Assert
            Assert.Equal(user, result);
            _mockUserRepo.Verify(x => x.GetByUsernameAsync(username), Times.Once());
        }

        [Theory]
        [InlineAutoData("Test")]
        [InlineAutoData("Test123")]
        public async Task GetByUsernameAsync_ShouldThrow_WhenUsernameDoesNotExist(string username)
        {
            //Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(() => _sut.GetByUsernameAsync(username));
            Assert.Equal(ex.Result.Message, string.Format(ExceptionMessages.NotFound, ExceptionMessages.User));
            _mockUserRepo.Verify(x => x.GetByUsernameAsync(username), Times.Once());
        }

        [Theory]
        [InlineAutoData("Test@abv.bg")]
        [InlineAutoData("Test123@gmail.com")]
        public async Task GetByEmailAsync_ShouldReturnUser_WhenEmailExists(string email)
        {
            //Arrange
            var user = new User()
            {
                Email = email
            };

            _mockUserRepo.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(user);

            //Act
            var result = await _sut.GetByEmailAsync(email);

            //Assert
            Assert.Equal(user, result);
            _mockUserRepo.Verify(x => x.GetByEmailAsync(email), Times.Once());
        }

        [Theory]
        [InlineAutoData("Test@abv.bg")]
        [InlineAutoData("Test123@gmail.com")]
        public async Task GetByEmailAsync_ShouldThrow_WhenEmailDoesNotExist(string email)
        {
            //Assert 
            var ex = Assert.ThrowsAsync<NotFoundException>(() => _sut.GetByEmailAsync(email));
            Assert.Equal(ex.Result.Message, string.Format(ExceptionMessages.NotFound, ExceptionMessages.User));
            _mockUserRepo.Verify(x => x.GetByEmailAsync(email), Times.Once());

        }

        [Theory]
        [InlineAutoData("Test")]
        [InlineAutoData("Test123")]
        public async Task GetByEmailOrUsernameAsync_ShouldReturnUser_WhenUsernameExists(string username)
        {
            //Arrange
            var user = new User()
            {
                Username = username
            };

            _mockUserRepo.Setup(x => x.GetByUsernameOrEmailAsync(username)).ReturnsAsync(user);

            //Act
            var result = await _sut.GetByEmailOrUsernameAsync(username);

            //Assert
            Assert.Equal(user, result);
            _mockUserRepo.Verify(x => x.GetByUsernameOrEmailAsync(username), Times.Once());
        }

        [Theory]
        [InlineAutoData("Test")]
        [InlineAutoData("Test123")]
        public async Task GetByEmailOrUsernameAsync_ShouldThrow_WhenUsernameDoesNotExist(string username)
        {
            //Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(() => _sut.GetByEmailOrUsernameAsync(username));
            Assert.Equal(ex.Result.Message, string.Format(ExceptionMessages.NotFound, ExceptionMessages.User));
            _mockUserRepo.Verify(x => x.GetByUsernameOrEmailAsync(username), Times.Once());
        }

        [Theory]
        [InlineAutoData("Test@abv.bg")]
        [InlineAutoData("Test123@gmail.com")]
        public async Task GetByEmailOrUsernameAsync_ShouldReturnUser_WhenEmailExists(string email)
        {
            //Arrange
            var user = new User()
            {
                Email = email
            };

            _mockUserRepo.Setup(x => x.GetByUsernameOrEmailAsync(email)).ReturnsAsync(user);

            //Act
            var result = await _sut.GetByEmailOrUsernameAsync(email);

            //Assert
            Assert.Equal(user, result);
            _mockUserRepo.Verify(x => x.GetByUsernameOrEmailAsync(email), Times.Once());
        }

        [Theory]
        [InlineAutoData("Test")]
        [InlineAutoData("Test123")]
        public async Task GetUserByClaimsAsync_ShouldReturnUser_WhenUserExists(string username)
        {
            //Arrange
            var user = new User()
            {
                Username = username
            };

            var userClaims = new ClaimsIdentity();
            userClaims.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", username));
            _mockUserRepo.Setup(x => x.GetByUsernameAsync(username)).ReturnsAsync(user);

            //Act
            var userResult = _sut.GetUserByClaimsAsync(userClaims).Result;

            //Assert
            Assert.Equal(userResult, user);
            _mockUserRepo.Verify(x => x.GetByUsernameAsync(username), Times.Once());
        }

        [Fact]
        public async Task GetUserByClaimsAsync_ShouldThrow_WhenClaimsAreNull()
        {
            //Assert
            Assert.ThrowsAsync<BusinessLogicException>(() => _sut.GetUserByClaimsAsync(null));
        }

        [Theory]
        [InlineAutoData("Test")]
        [InlineAutoData("Test123")]
        public async Task GetUserByClaimsAsync_ShouldThrow_WhenUsernameDoesNotExist(string username)
        {
            //Arrange
            var userClaims = new ClaimsIdentity();
            userClaims.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", username));

            //Asset
            Assert.ThrowsAsync<NotFoundException>(() => _sut.GetUserByClaimsAsync(userClaims));
            _mockUserRepo.Verify(x => x.GetByUsernameAsync(username), Times.Once);
        }
    }
}
