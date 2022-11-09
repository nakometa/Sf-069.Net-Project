using FluentValidation;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.DAL.Data.Configurations.Constants;

namespace SportsHub.Api.Validations
{
    public class CommentValidator : AbstractValidator<PostCommentDTO>
    {
        public CommentValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required")
                .Length(0, ConfigurationConstants.CommentContentMaxLength)
                .WithMessage($"Content should be less than {ConfigurationConstants.CommentContentMaxLength} characters.");

            RuleFor(x => x.AuthorId)
                .NotEmpty().WithMessage("Author ID is required");

            RuleFor(x => x.ArticleId)
                .NotEmpty().WithMessage("Article ID is required");
        }
    }
}
