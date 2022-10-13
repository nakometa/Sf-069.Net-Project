using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SportsHub.Domain.Models.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(t => t.Email).EmailAddress().Length(0, 100);
            // Username must contain at least 6 characters
            RuleFor(t => t.Username).NotEmpty().Length(6, 100);
            RuleFor(t => t.DisplayName).NotEmpty().Length(0, 100);
            RuleFor(t => t.FirstName).NotEmpty().Length(0, 100);
            RuleFor(t => t.LastName).NotEmpty().Length(0, 100);
            // Password must contain at least 8 characters
            RuleFor(t => t.Password).NotEmpty().Length(8, 100);
        }
    }
}
