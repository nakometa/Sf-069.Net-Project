using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsHub.Api.Mapping.Models;
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

        public async Task<ActionResult> DeleteArticleAsync(int id)
        {
            bool deletedSuccessful = await _articleService.DeleteArticleAsync(id);
            
            if (deletedSuccessful) return Ok("Article deleted successful");
            
            return BadRequest("Unable to delete article");
        }
    }
}
