using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SimpleUser.MVC.DTOs;

namespace SimpleUser.MVC.Validators
{
    public static class Extensions
    {
        public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }

        public static void AddToModelState(this ValidationErrorDto result, ModelStateDictionary modelState)
        {
            if(result.errors.Email != null)
            {
                foreach (var error in result.errors.Email)
                {
                    modelState.AddModelError("Email", error);
                }
            }
            if(result.errors.OldPassword != null)
            {
                foreach (var error in result.errors.OldPassword)
                {
                    modelState.AddModelError("OldPassword", error);
                }
            }
        }
    }
}
