using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsHub.Api.Mapping.Models;
using SportsHub.Api.Validations;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.Constants;

namespace SportsHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportController : ControllerBase
    {
        private readonly IValidator<CreateSportDTO> _sportValidator;
        private readonly ISportService _sportService;
        private readonly IMapper _mapper;

        public SportController(IValidator<CreateSportDTO> sportValidator,
                               ISportService sportService,
                               IMapper mapper)
        {
            _sportValidator = sportValidator;
            _sportService = sportService;
            _mapper = mapper;
        }

        [HttpGet("GetAllSports")]
        public async Task<ActionResult<IEnumerable<SportResponseDTO>>> GetAllAsync()
        {
            var sports = await _sportService.GetAllAsync();

            var sportsResponse = _mapper.Map<List<SportResponseDTO>>(sports);
            return Ok(sportsResponse);
        }

        [HttpGet("GetSportById")]
        public async Task<IActionResult> GetSportByIdAsync(int id)
        {
            var sportResult = await _sportService.GetByIdAsync(id);

            var sportsResponse = _mapper.Map<SportResponseDTO>(sportResult);
            return Ok(sportsResponse);
        }


        [HttpGet("GetSportByName")]
        public async Task<IActionResult> GetSportByNameAsync(string sport)
        {
            var sportResult = await _sportService.GetByNameAsync(sport);

            var sportResponse = _mapper.Map<SportResponseDTO>(sportResult);
            return Ok(sportResponse);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddSport")]
        public async Task<IActionResult> CreateSportAsync([FromBody] CreateSportDTO sportDTO)
        {
            ValidationResult validationResult = await _sportValidator.ValidateAsync(sportDTO);

            if (!validationResult.IsValid)
            {
                return ValidationProblem(validationResult.ToString("~"));
            }

            await _sportService.CreateSportAsync(sportDTO);
            return Ok(ValidationMessages.SportAddedSuccessfully);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("EditSport")]
        public async Task<IActionResult> EditSportAsync([FromBody] CreateSportDTO sportDTO)
        {
            ValidationResult validationResult = await _sportValidator.ValidateAsync(sportDTO);
            
            if (!validationResult.IsValid)
            {
                return ValidationProblem(validationResult.ToString("~"));
            }

            await _sportService.EditSportAsync(sportDTO);
            return Ok(ValidationMessages.SportUpdatedSuccessfully);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteSport")]
        public async Task<ActionResult> DeleteSportAsync(int id)
        {
            await _sportService.DeleteSportAsync(id);
            return Ok(ValidationMessages.SportDeletedSuccessfully);
        }
    }
}
