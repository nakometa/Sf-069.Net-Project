using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SportsHub.Api.Validations
{
    public class GenerateModelStateDictionary : IGenerateModelStateDictionary
    {
        public ModelStateDictionary modelStateDictionary(ValidationResult validationResult)
        {
            ModelStateDictionary modelStateDictionary = new ModelStateDictionary();

            foreach (ValidationFailure failure in validationResult.Errors)
            {
                modelStateDictionary.AddModelError(failure.PropertyName, failure.ErrorMessage);
            }

            return modelStateDictionary;
        }
    }
}
