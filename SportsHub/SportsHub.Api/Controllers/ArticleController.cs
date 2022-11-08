using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsHub.Api.Mapping.Models;
using SportsHub.AppService.Services;
using SportsHub.Domain.Constants;

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

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ArticleResponseDTO>>> GetAllAsync()
        {
            var articles = await _articleService.GetAllAsync();

            if (!articles.Any())
            {
                return BadRequest(ValidationMessages.NoArticles);
            }

            var articlesResponse = _mapper.Map<List<ArticleResponseDTO>>(articles);
            return Ok(articlesResponse);
        }

        [HttpGet("GetArticleByTitle")]
        public async Task<ActionResult<ArticleResponseDTO>> GetArticleByTitleAsync(string title)
        {
            var article = await _articleService.GetByTitleAsync(title);

            if (article == null)
            {
                return BadRequest(ValidationMessages.NoSuchArticle);
            }

            var articleResponse = _mapper.Map<ArticleResponseDTO>(article);
            return Ok(articleResponse);
        }
    }
}
