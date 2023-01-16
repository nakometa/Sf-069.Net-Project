using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsHub.Api.Validations;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.Constants;
using SportsHub.Domain.Models;

namespace SportsHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCommentDTO> _commentValidator;
        private readonly IGenerateModelStateDictionary _generateModelStateDictionary;

        public CommentController(ICommentService service,
                                 IMapper mapper,
                                 IValidator<CreateCommentDTO> commentValidator,
                                 IGenerateModelStateDictionary generateModelStateDictionary)
        {
            _commentService = service;
            _mapper = mapper;
            _commentValidator = commentValidator;
            _generateModelStateDictionary = generateModelStateDictionary;
        }

        [HttpGet("GetByArticle")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetByArticleAsync(int articleId)
        {
            var comments = await _commentService.GetByArticleAsync(articleId);

            if (!comments.Any())
            {
                return Ok(ValidationMessages.NoCommentsForArticle);
            }

            return Ok(comments);
        }

        [Authorize]
        [HttpPost("Post")]
        public async Task<ActionResult> PostCommentAsync([FromBody] CreateCommentDTO commentInput)
        {
            var validationResult = await _commentValidator.ValidateAsync(commentInput);

            if (!validationResult.IsValid)
            {
                var response = _generateModelStateDictionary.modelStateDictionary(validationResult);
                return ValidationProblem(response);
            }

            await _commentService.AddCommentAsync(commentInput);                       

            return Created(ValidationMessages.CommentAddedSuccessfully, commentInput);
        }

        [Authorize]
        [HttpPost("Like")]
        public async Task<ActionResult> LikeCommentAsync([FromBody] LikeCommentDTO likeCommentDTO)
        {
            await _commentService.LikeCommentAsync(likeCommentDTO);

            return Created(ValidationMessages.CommentLikedSuccessfully, likeCommentDTO);
        }
    }
}
