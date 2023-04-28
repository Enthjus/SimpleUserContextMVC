using FluentValidation;
using SimpleUser.API.DTOs;

namespace SimpleUser.API.Validators
{
    public class SignInValidatior : AbstractValidator<SignInDto>
    {
        public SignInValidatior()
        {
            RuleFor(x => x.Email)
               .NotNull()
               .WithMessage("Email cannot be empty")
               .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
               .WithMessage("Please enter a valid email address");
            RuleFor(x => x.Password)
               .NotNull()
               .WithMessage("Password cannot be empty")
               .MaximumLength(50)
               .WithMessage("Password should no more than 50 character");
        }
    }
}
