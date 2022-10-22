using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;
using System.Text.Json;

namespace SportsHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return Ok($"{JsonSerializer.Serialize(articleResponse)}");
        }
    }
}
