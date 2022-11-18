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
            _commentController = new CommentController(_commentService.Object, _mapper, _commentValidator.Object, _generateModelStateDictionary.Object);
        }

        [Fact]
        public async Task GetByArticleAsync_CommentsForProvidedActicleExist_ReturnsOkStatus()
        {
            //Arrange
            var comments = CommentMockData.GetForArticle();
            _commentService.Setup(service => service.GetByArticleAsync(TestArticleId)).ReturnsAsync(comments);

            //Act
            var result = await _commentController.GetByArticleAsync(TestArticleId);

            //Assert
            var resultObject = TestHelper.GetObjectResultContent<IEnumerable<CreateCommentRequest>>(result);

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(NumberOfTestComments, resultObject.Count());
        }

        [Fact]
        public async Task GetByArticleAsync_CommentsForProvidedActicleDoNotExist_ReturnsOkStatus()
        {
            //Arrange
            _commentService.Setup(service => service.GetByArticleAsync(TestArticleId)).ReturnsAsync(new List<Comment>());

            //Act
            var result = await _commentController.GetByArticleAsync(TestArticleId);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
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
