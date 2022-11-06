using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsHub.AppService.Services;

namespace SportsHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;
        private readonly IMapper _mapper;

        public CommentController(ICommentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("GetByArticle")]
        public async Task<IActionResult> GetByArticleAsync(int articleId)
        {
            var comments = await _service.GetByArticleAsync(articleId);

            if (!comments.Any())
            {
                return BadRequest($"No comments for this article");
            }

            return Ok(comments);
        }
    }
}
