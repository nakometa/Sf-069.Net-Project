using FluentValidation;
using SportsHub.Domain.Models;

namespace SportsHub.Api.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(t => t.Email).EmailAddress().Length(0, 100);
            RuleFor(t => t.Username).NotEmpty().Length(6, 100);
            RuleFor(t => t.DisplayName).NotEmpty().Length(0, 100);
            RuleFor(t => t.FirstName).NotEmpty().Length(0, 100);
            RuleFor(t => t.LastName).NotEmpty().Length(0, 100);
            RuleFor(t => t.Password).NotEmpty().Length(8, 100);
        }
    }
}
