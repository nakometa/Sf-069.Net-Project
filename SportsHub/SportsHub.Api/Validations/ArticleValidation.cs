﻿using FluentValidation;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.DAL.Data.Configurations.Constants;

namespace SportsHub.Api.Validations
{
    public class ArticleValidation : AbstractValidator<CreateArticleDTO>
    {
        public ArticleValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(0, ConfigurationConstants.articleTitleMaxLength)
                .WithMessage($"Title should be less than {ConfigurationConstants.articleTitleMaxLength} characters.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
        }
    }
}
