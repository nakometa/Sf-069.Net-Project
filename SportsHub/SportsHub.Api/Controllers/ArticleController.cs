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
        private readonly IFilterService _filterService;
        private readonly IMapper _mapper;

        public ArticleController(IArticleService service, IMapper mapper, IFilterService filterService)
        {
            _articleService = service;
            _mapper = mapper;
            _filterService = filterService;
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

        [HttpGet("GetArticlesBySubstring")]
        public async Task<ActionResult> GetArticlesBySubstring(string substring)
        {
            var articles = await _filterService.GetListOfArticlesBySubstring(substring);
            
            if (articles.Count < 1)
            {
                return BadRequest($"No match found for substring {substring}");
            }

            //var articleResponse = _mapper.Map<ArticleResponseDTO>(article);
            return Ok();
        }
    }
}
