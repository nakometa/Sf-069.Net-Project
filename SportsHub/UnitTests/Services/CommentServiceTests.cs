using Moq;
using SportsHub.AppService.Services;
using SportsHub.Domain.Repository;
using SportsHub.Domain.UOW;

namespace UnitTests.Services
{
    public class CommentServiceTests
    {
        private readonly CommentService _commentService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICommentRepository> _repository;

        public CommentServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repository = new Mock<ICommentRepository>();
            _unitOfWorkMock.Setup(u => u.CommentRepository).Returns(_repository.Object);
            _commentService = new CommentService(_unitOfWorkMock.Object);
        }
    }
}
