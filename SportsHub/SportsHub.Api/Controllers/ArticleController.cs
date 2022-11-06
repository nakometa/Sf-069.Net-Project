using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsHub.Api.Mapping.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        private readonly IValidator<CreateArticleDTO> _articleValidator;

        public ArticleController(IArticleService service, IMapper mapper, IValidator<CreateArticleDTO> articleValidator)
        {
            _articleService = service;
            _mapper = mapper;
            _articleValidator = articleValidator;
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
                var modelStateDictionary = new ModelStateDictionary();

                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    modelStateDictionary.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }

                return ValidationProblem(modelStateDictionary);
            }

            bool createdSuccessful = await _articleService.CreateArticleAsync(adminInput);

            if(createdSuccessful)
            {
                return Ok("Article created successfully.");
            }

            return BadRequest("Unable to create article.");
        }
    }
}
