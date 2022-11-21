using Moq;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using SportsHub.Domain.Repository;
using SportsHub.Domain.UOW;
using UnitTests.MockData;
using Xunit;

namespace UnitTests.Services
{
    public class CommentServiceTests
    {
        private readonly int TestUserId = 14;
        private readonly int TestArticleId = 5;
        private readonly int TestCommentId = 1;
        private readonly CommentService _commentService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IArticleRepository> _articleRepository;
        private readonly Mock<ICommentRepository> _commentRepository;

        public CommentServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userRepository = new Mock<IUserRepository>();
            _articleRepository = new Mock<IArticleRepository>();
            _commentRepository = new Mock<ICommentRepository>();

            _unitOfWorkMock.Setup(u => u.UserRepository).Returns(_userRepository.Object);
            _unitOfWorkMock.Setup(u => u.ArticleRepository).Returns(_articleRepository.Object);
            _unitOfWorkMock.Setup(u => u.CommentRepository).Returns(_commentRepository.Object);

            _commentService = new CommentService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task AddCommentAsync_WithExistingArticleAndExistingAuthor_ReturnsTrue()
        {
            //Arange
            _userRepository.Setup(repo => repo.GetById(TestUserId)).Returns(UserMockData.GetUser());
            _articleRepository.Setup(repo => repo.GetById(TestArticleId)).Returns(ArticleMockData.GetArticle());

            //Act
            var result = await _commentService.AddCommentAsync(CommentMockData.GetCommentDTO());

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddCommentAsync_WithNonExistingArticleAndExistingAuthor_ReturnsFalse()
        {
            //Arange
            _userRepository.Setup(repo => repo.GetById(TestUserId)).Returns(UserMockData.GetUser());
            _articleRepository.Setup(repo => repo.GetById(TestArticleId)).Returns((Article?)null);

            //Act
            var result = await _commentService.AddCommentAsync(CommentMockData.GetCommentDTO());

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task AddCommentAsync_WithExistingArticleAndNonExistingAuthor_ReturnsFalse()
        {
            //Arange
            _userRepository.Setup(repo => repo.GetById(TestUserId)).Returns((User?)null);
            _articleRepository.Setup(repo => repo.GetById(TestArticleId)).Returns(ArticleMockData.GetArticle());

            //Act
            var result = await _commentService.AddCommentAsync(CommentMockData.GetCommentDTO());

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task AddCommentAsync_WithNonExistingArticleAndNonExistingAuthor_ReturnsFalse()
        {
            //Arange
            _userRepository.Setup(repo => repo.GetById(TestUserId)).Returns((User?)null);
            _articleRepository.Setup(repo => repo.GetById(TestArticleId)).Returns((Article?)null);

            //Act
            var result = await _commentService.AddCommentAsync(CommentMockData.GetCommentDTO());

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task LikeCommentAsync_WithExistingComment_ReturnsTrue()
        {
            //Arrange
            _commentRepository.Setup(repo => repo.GetById(TestCommentId)).Returns(CommentMockData.GetComment());

            //Act
            var result = await _commentService.LikeCommentAsync(TestCommentId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task LikeCommentAsync_WithNonExistingComment_ReturnsFalse()
        {
            //Arrange
            _commentRepository.Setup(repo => repo.GetById(TestCommentId)).Returns((Comment?)null);

            //Act
            var result = await _commentService.LikeCommentAsync(TestCommentId);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DislikeCommentAsync_WithExistingComment_ReturnsTrue()
        {
            //Arrange
            _commentRepository.Setup(repo => repo.GetById(TestCommentId)).Returns(CommentMockData.GetComment());

            //Act
            var result = await _commentService.DislikeCommentAsync(TestCommentId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DislikeCommentAsync_WithNonExistingComment_ReturnsFalse()
        {
            //Arrange
            _commentRepository.Setup(repo => repo.GetById(TestCommentId)).Returns((Comment?)null);

            //Act
            var result = await _commentService.DislikeCommentAsync(TestCommentId);

            //Assert
            Assert.False(result);
        }
    }
}
