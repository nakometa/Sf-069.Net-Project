using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsHub.Api.DTOs;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;

namespace SportsHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService service;
        private readonly IMapper mapper;

        public ArticleController(IArticleService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet("GetArticleByTitle")]
        public async Task<IActionResult> GetArticleByTitleAsync(string title)
        {
            var article = await service.GetByTitleAsync(title);

            if (article == null)
            {
                return BadRequest($"No such article");
            }

            ArticleResponseDTO articleResponse = mapper.Map<ArticleResponseDTO>(article);
            return Ok($"Article: {articleResponse.Title}");
        }
    }
}
