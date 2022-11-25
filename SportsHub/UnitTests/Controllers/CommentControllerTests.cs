using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsHub.Api.Controllers;
using SportsHub.Api.Mapping.Models;
using SportsHub.Api.Validations;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using System.Runtime.CompilerServices;
using UnitTests.MockData;
using UnitTests.Utils;
using Xunit;

namespace UnitTests.Controllers
{
    public class CommentControllerTests
    {
        private IMapper _mapper;
        private readonly CommentController _commentController;
        private readonly Mock<ICommentService> _commentService;
        private readonly Mock<IValidator<CreateCommentDTO>> _commentValidator;
        private readonly Mock<IGenerateModelStateDictionary> _generateModelStateDictionary;
        private readonly Fixture _fixture;
        private readonly int TestArticleId = 5;
        private readonly int TestCommentId = 1;
        private readonly int NumberOfTestComments = 3;

        public CommentControllerTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(x =>
                {
                    x.CreateMap<Comment, CreateCommentDTO>();
                    x.CreateMap<Comment, CreateCommentRequest>();
                    x.CreateMap<InputCommentDTO, CreateCommentDTO>();

                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _commentService = new Mock<ICommentService>();
            _commentValidator = new Mock<IValidator<CreateCommentDTO>>();
            _generateModelStateDictionary = new Mock<IGenerateModelStateDictionary>();
            _fixture = new Fixture();
            _commentController = new CommentController(_commentService.Object, _mapper, _commentValidator.Object, _generateModelStateDictionary.Object);
        }

        [Theory]
        [AutoMoqData]
        public void GetByArticle_CommentsForProvidedActicleExist_ReturnsOkStatus([Frozen] Mock<ICommentService> commentService, [NoAutoProperties] CommentController commentController, int commentsCount)
        {
            //Arrange
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var comments = _fixture.Build<Comment>().CreateMany(commentsCount).ToList();
            commentService.Setup(service => service.GetByArticle(It.IsAny<int>(), It.IsAny<CategoryParameters>())).Returns(comments);

            //Act
            var result = commentController.GetByArticle(It.IsAny<CategoryParameters>(), It.IsAny<int>()).Result; 

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(comments.Count(), commentsCount);
        }

        [Theory]
        [AutoMoqData]
        public void GetByArticleAsync_CommentsForProvidedActicleDoNotExist_ReturnsNotFound([Frozen] Mock<ICommentService> commentService, [NoAutoProperties] CommentController commentController)
        {
            //Arrange
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            commentService.Setup(service => service.GetByArticle(It.IsAny<int>(), It.IsAny<CategoryParameters>())).Throws<Exception>();

            //Assert
            Assert.Throws<Exception>(() => commentController.GetByArticle(It.IsAny<CategoryParameters>(), It.IsAny<int>()));
        }

        [Fact]
        public async Task PostCommentAsync_NewComment_ReturnsOkStatus()
        {
            //Arrange
            var commentDTO = CommentMockData.GetCommentDTO();
            var inputComment = CommentMockData.GetCommentDTO;
            _commentService.Setup(service => service.AddCommentAsync(commentDTO)).ReturnsAsync(true);
            _commentValidator.Setup(validator => validator.ValidateAsync(inputComment, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());

            //Act
            var result = await _commentController.PostCommentAsync(inputComment);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task PostCommentAsync_NewComment_ReturnsBadRequest()
        {
            //Arrange
            var commentDTO = CommentMockData.GetCommentDTO();
            var inputComment = CommentMockData.GetCommentDTO();
            _commentService.Setup(service => service.AddCommentAsync(commentDTO)).ReturnsAsync(false);
            _commentValidator.Setup(validator => validator.ValidateAsync(inputComment, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());

            //Act
            var result = await _commentController.PostCommentAsync(inputComment);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task LikeCommentAsync_WithExistingComment_ReturnsOkStatus()
        {
            //Arrange
            _commentService.Setup(service => service.LikeCommentAsync(TestCommentId)).ReturnsAsync(true);

            //Act
            var result = await _commentController.LikeCommentAsync(TestCommentId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task LikeCommentAsync_WithNonExistingComment_ReturnsBadRequest()
        {
            //Arrange
            _commentService.Setup(service => service.LikeCommentAsync(TestCommentId)).ReturnsAsync(false);

            //Act
            var result = await _commentController.LikeCommentAsync(TestCommentId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DislikeCommentAsync_WithExistingComment_ReturnsOkStatus()
        {
            //Arrange
            _commentService.Setup(service => service.DislikeCommentAsync(TestCommentId)).ReturnsAsync(true);

            //Act
            var result = await _commentController.DislikeCommentAsync(TestCommentId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DislikeCommentAsync_WithNonExistingComment_ReturnsBadRequest()
        {
            //Arrange
            _commentService.Setup(service => service.DislikeCommentAsync(TestCommentId)).ReturnsAsync(false);

            //Act
            var result = await _commentController.DislikeCommentAsync(TestCommentId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
