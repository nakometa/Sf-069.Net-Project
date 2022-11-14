using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsHub.Api.Mapping.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.Constants;
using SportsHub.Api.Validations;

namespace SportsHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateArticleDTO> _articleValidator;
        private readonly IGenerateModelStateDictionary _generateModelStateDictionary;

        public ArticleController(IArticleService service, IMapper mapper, 
                                 IValidator<CreateArticleDTO> articleValidator,
                                 IGenerateModelStateDictionary generateModelStateDictionary)
        {
            _articleService = service;
            _mapper = mapper;
            _articleValidator = articleValidator;
            _generateModelStateDictionary = generateModelStateDictionary;
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

        [Authorize(Roles = "Admin")]
        [HttpPost("AddArticle")]
        public async Task<IActionResult> CreateArticleAsync([FromBody] CreateArticleDTO adminInput)
        {
            ValidationResult validationResult = await _articleValidator.ValidateAsync(adminInput);
            if(!validationResult.IsValid)
            {
                var response = _generateModelStateDictionary.modelStateDictionary(validationResult);

                return ValidationProblem(response);
            }

            bool createdSuccessful = await _articleService.CreateArticleAsync(adminInput);

            if(createdSuccessful)
            {
                return Ok(ValidationMessages.ArticleCreatedSuccessfully);
            }

            return BadRequest(ValidationMessages.UnableToCreateArticle);
        }
        
        [HttpGet("GetArticlesBySubstring")]
        public async Task<ActionResult<List<ArticleResponseDTO>>> GetArticlesBySubstring(string substring)
        {
            var articles = await _articleService.GetListOfArticlesBySubstringAsync(substring);

            var articleResponse = _mapper.Map<List<ArticleResponseDTO>>(articles);
            return Ok(articleResponse);
        }
    }
}