using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsHub.Api.Mapping.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;
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
                return Ok("Article created successfully.");
            }

            return BadRequest("Unable to create article.");
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

            bool editedSuccessfylly = await _articleService.EditArticle(adminInput);

            if (editedSuccessfylly)
            {
                return Ok("Article updated successfully.");
            }

            return BadRequest("Article not found.");
        }
    }
}
