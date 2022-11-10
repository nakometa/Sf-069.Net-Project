using AutoMapper;
using FluentValidation;
using Moq;
using SportsHub.Api.Controllers;
using SportsHub.Api.Mapping.Models;
using SportsHub.Api.Validations;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;

namespace UnitTests.Controllers
{
    public class CommentControllerTests
    {
        private IMapper _mapper;
        private readonly CommentController _commentController;
        private readonly Mock<ICommentService> _commentService;
        private readonly Mock<IValidator<PostCommentDTO>> _commentValidator;
        private readonly Mock<IGenerateModelStateDictionary> _generateModelStateDictionary;

        public CommentControllerTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(x => x.CreateMap<Article, ArticleResponseDTO>());
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _commentService = new Mock<ICommentService>();
            _commentValidator = new Mock<IValidator<PostCommentDTO>>();
            _generateModelStateDictionary = new Mock<IGenerateModelStateDictionary>();
            _commentController = new CommentController(_commentService.Object, _mapper, _commentValidator.Object, _generateModelStateDictionary.Object);
        }
    }
}
