using Microsoft.AspNetCore.Mvc.ModelBinding;
using FluentValidation.Results;

namespace SportsHub.Api.Validations
{
    public interface IGenerateModelStateDictionary
    {
        ModelStateDictionary modelStateDictionary(ValidationResult validationResult);
    }
}
