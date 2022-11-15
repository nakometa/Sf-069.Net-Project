using FluentValidation;
using SportsHub.Api.Mapping.Models;
using SportsHub.DAL.Data.Configurations.Constants;
using SportsHub.Domain.Constants;

namespace SportsHub.Api.Validations
{
    public class CommentValidator : AbstractValidator<InputCommentDTO>
    {
        public CommentValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(ValidationMessages.CommentContentValidationNotEmptyMessage)
                .Length(0, ConfigurationConstants.CommentContentMaxLength)
                .WithMessage(string.Format(ValidationMessages.CommentContentValidationLengthMessage, ConfigurationConstants.CommentContentMaxLength.ToString()));

            RuleFor(x => x.AuthorId)
                .NotEmpty().WithMessage(ValidationMessages.CommentAuthorValidationMessage);

            RuleFor(x => x.ArticleId)
                .NotEmpty().WithMessage(ValidationMessages.CommentArticleValidationMessage);
        }
    }
}
