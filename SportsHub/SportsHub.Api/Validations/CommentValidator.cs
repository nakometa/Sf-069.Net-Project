using FluentValidation;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.DAL.Data.Configurations.Constants;
using SportsHub.Domain.Constants;

namespace SportsHub.Api.Validations
{
    public class CommentValidator : AbstractValidator<CreateCommentDTO>
    {
        public CommentValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(ValidationMessages.CommentContentValidationNotEmptyMessage)
                .Length(0, ConfigurationConstants.CommentContentMaxLength)
                .WithMessage(ValidationMessages.CommentContentValidationLengthMessage.Replace("[0]", ConfigurationConstants.CommentContentMaxLength.ToString()));

            RuleFor(x => x.AuthorId)
                .NotEmpty().WithMessage("Author ID is required");

            RuleFor(x => x.ArticleId)
                .NotEmpty().WithMessage("Article ID is required");
        }
    }
}
