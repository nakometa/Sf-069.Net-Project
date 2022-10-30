using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;

namespace SportsHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;

        public ArticleController(IArticleService service, IMapper mapper)
        {
            _articleService = service;
            _mapper = mapper;
        }

        [HttpGet("GetArticleByTitle")]
        public async Task<ActionResult<ArticleResponseDTO>> GetArticleByTitleAsync(string title)
        {
            var article = await _articleService.GetByTitleAsync(title);

            if (article == null)
            {
                return BadRequest($"No such article");
            }

            var articleResponse = _mapper.Map<ArticleResponseDTO>(article);
            return Ok(articleResponse);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateArticleAsync([FromBody] CreateArticleDTO adminInput)
        {
            bool createdSuccessful = await _articleService.CreateArticleAsync(adminInput);

            if(createdSuccessful)
            {
                return Ok("Article created successfully.");
            }

            return BadRequest("Unable to create article.");
        }
    }
}
