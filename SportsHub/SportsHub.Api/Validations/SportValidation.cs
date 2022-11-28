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
                .NotEmpty().WithMessage(ConfigurationConstants.SportConstants.SportNameIsRequired)
                .MaximumLength(ConfigurationConstants.SportConstants.SportNameMaxLength)
                .WithMessage(string.Format(ConfigurationConstants.SportConstants.SportNameMaxLengthMessage, ConfigurationConstants.SportConstants.SportNameMaxLength));

            RuleFor(x => x.Description)
                .MaximumLength(ConfigurationConstants.SportConstants.SportDescriptionMaxLength)
                .WithMessage(string.Format(ConfigurationConstants.SportConstants.SportDescriptionMaxLengthMessage, ConfigurationConstants.SportConstants.SportDescriptionMaxLength));
        }
    }
}
