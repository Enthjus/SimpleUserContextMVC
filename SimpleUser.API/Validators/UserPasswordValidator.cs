using FluentValidation;
using SimpleUser.API.DTOs;

namespace SimpleUser.API.Validators
{
    public class UserPasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public UserPasswordValidator()
        {
            RuleFor(x => x.CurrentPassword)
               .NotNull()
               .WithMessage("Password cannot be empty")
               .MaximumLength(50)
               .WithMessage("Password should no more than 50 character");
            RuleFor(x => x.NewPassword)
               .NotNull()
               .WithMessage("Password cannot be empty")
               .MaximumLength(50)
               .WithMessage("Password should no more than 50 character");
        }
    }
}
