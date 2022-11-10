using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SportsHub.Api.Validations;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;

namespace SportsHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        private readonly IValidator<PostCommentDTO> _commentValidator;
        private readonly IGenerateModelStateDictionary _generateModelStateDictionary;

        public CommentController(ICommentService service, IMapper mapper, IValidator<PostCommentDTO> commentValidator, IGenerateModelStateDictionary generateModelStateDictionary)
        {
            _commentService = service;
            _mapper = mapper;
            _commentValidator = commentValidator;
            _generateModelStateDictionary = generateModelStateDictionary;
        }

        [HttpGet("GetByArticle")]
        public async Task<IActionResult> GetByArticleAsync(int articleId)
        {
            var comments = await _commentService.GetByArticleAsync(articleId);

            if (!comments.Any())
            {
                return BadRequest($"No comments for this article");
            }

            return Ok(comments);
        }

        //[Authorize]
        [HttpPost("PostComment")]
        public async Task<ActionResult> PostCommentAsync([FromBody] PostCommentDTO commentInput)
        {
            var validationResult = await _commentValidator.ValidateAsync(commentInput);

            if (!validationResult.IsValid)
            {
                var response = _generateModelStateDictionary.modelStateDictionary(validationResult);
                return ValidationProblem(response);
            }

            var created = await _commentService.PostCommentAsync(commentInput);

            if (!created)
            {
                return BadRequest("Unable to post comment.");
            }

            return Ok("Comment posted successfully.");
        }
    }
}
