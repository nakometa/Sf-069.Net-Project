using FluentValidation;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.DAL.Data.Configurations.Constants;

namespace SportsHub.Api.Validations
{
    public class SportValidation : AbstractValidator<CreateSportDTO>
    {
        public SportValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Sport name is required.")
                .MaximumLength(ConfigurationConstants.SportConstants.SportNameMaxLength)
                .WithMessage($"The maximum length of Name is {ConfigurationConstants.SportConstants.SportNameMaxLength} characters");

            RuleFor(x => x.Description)
                .MaximumLength(ConfigurationConstants.SportConstants.SportDescriptionMaxLength)
                .WithMessage($"The maximum length of Description is {ConfigurationConstants.SportConstants.SportDescriptionMaxLength} characters");
        }
    }
}
