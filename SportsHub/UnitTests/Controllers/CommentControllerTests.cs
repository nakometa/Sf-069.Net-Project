using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsHub.Api.Controllers;
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
        private readonly Mock<IValidator<PostCommentDTO>> _commentValidator;
        private readonly Mock<IGenerateModelStateDictionary> _generateModelStateDictionary;
        private readonly int TestArticleId = 5;
        private readonly int NumberOfTestComments = 3;

        public CommentControllerTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(x => x.CreateMap<Comment, PostCommentDTO>());
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _commentService = new Mock<ICommentService>();
            _commentValidator = new Mock<IValidator<PostCommentDTO>>();
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
            var resultObject = TestHelper.GetObjectResultContent<IEnumerable<Comment>>(result);

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
            var comment = CommentMockData.GetComment();
            _commentService.Setup(service => service.PostCommentAsync(comment)).ReturnsAsync(true);
            _commentValidator.Setup(validator => validator.ValidateAsync(comment, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());

            //Act
            var result = await _commentController.PostCommentAsync(comment);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task PostCommentAsync_NewComment_ReturnsBadRequest()
        {
            //Arrange
            var comment = CommentMockData.GetComment();
            _commentService.Setup(service => service.PostCommentAsync(comment)).ReturnsAsync(false);
            _commentValidator.Setup(validator => validator.ValidateAsync(comment, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());

            //Act
            var result = await _commentController.PostCommentAsync(comment);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
