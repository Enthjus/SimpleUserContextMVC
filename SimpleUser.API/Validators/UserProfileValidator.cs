using FluentValidation;
using SimpleUser.API.DTOs;

namespace SimpleUser.API.Validators
{
    public class UserProfileValidator : AbstractValidator<UserProfileDto>
    {
        public UserProfileValidator()
        {
            RuleFor(x => x.FirstName)
             .NotNull()
             .WithMessage("FirstName cannot be empty");
            RuleFor(x => x.LastName)
             .NotNull()
             .WithMessage("LastName cannot be empty");
            RuleFor(x => x.PhoneNumber)
             .NotNull()
             .WithMessage("PhoneNumber cannot be empty");
        }
    }
}
