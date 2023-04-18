using FluentValidation;

namespace SimpleUser.API.Validators
{
    public abstract class BaseValidationModel<T> : IBaseValidationModel
    {
        public void Validate(object validator, IBaseValidationModel modelObj)
        {
            var instance = (IValidator<T>)validator;
            var result = instance.Validate((T)modelObj);

            if (!result.IsValid && result.Errors.Any())
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
