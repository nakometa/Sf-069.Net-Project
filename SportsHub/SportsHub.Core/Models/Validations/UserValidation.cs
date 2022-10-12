using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SportsHub.Domain.Models.Validations
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(t => t.Email).EmailAddress();
            RuleFor(t => t.Username).NotEmpty();
            RuleFor(t => t.DisplayName).NotEmpty();
            RuleFor(t => t.FirstName).NotEmpty();
            RuleFor(t => t.LastName).NotEmpty();
            RuleFor(t => t.Password).NotEmpty();
        }
    }
}
