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
using SportsHub.Domain.Models.Constants;

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

        //[Authorize(Roles = "Admin")]
        [HttpPost("AddArticle")]
        public async Task<IActionResult> CreateArticleAsync([FromBody] CreateArticleDTO adminInput)
        {
            ValidationResult validationResult = await _articleValidator.ValidateAsync(adminInput);
            if (!validationResult.IsValid)
            {
                var response = _generateModelStateDictionary.modelStateDictionary(validationResult);

                return ValidationProblem(response);
            }

            string createdSuccessful = await _articleService.CreateArticleAsync(adminInput);

            return Created(String.Empty, createdSuccessful);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut("EditArticle")]
        public async Task<IActionResult> EditArticleAsync([FromBody] CreateArticleDTO adminInput)
        {
            ValidationResult validationResult = await _articleValidator.ValidateAsync(adminInput);
            if (!validationResult.IsValid)
            {
                var response = _generateModelStateDictionary.modelStateDictionary(validationResult);

                return ValidationProblem(response);
            }

            bool editedSuccessfully = await _articleService.EditArticleAsync(adminInput);

            if (editedSuccessfully)
            {
                return Ok(ValidationMessages.ArticleUpdatedSuccessfully);
            }

            return BadRequest(ValidationMessages.UnableToUpdateArticle);
        }
        
        [HttpGet("GetArticlesBySubstring")]
        public async Task<ActionResult<List<ArticleResponseDTO>>> GetArticlesBySubstring(string substring)
        {
            var articles = await _articleService.GetListOfArticlesBySubstringAsync(substring);

            var articleResponse = _mapper.Map<List<ArticleResponseDTO>>(articles);
            return Ok(articleResponse);
        }

        [HttpDelete("DeleteArticle")]
        public async Task<ActionResult> DeleteArticleAsync(int id)
        {
            await _articleService.DeleteArticleAsync(id);

            return Ok(ValidationMessages.ArticleDeletedSuccessfully);
        }
    }
}