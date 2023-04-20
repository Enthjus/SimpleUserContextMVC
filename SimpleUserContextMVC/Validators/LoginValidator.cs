using FluentValidation;
using SimpleUser.MVC.DTOs;

namespace SimpleUser.MVC.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
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
