using FluentValidation;
using SimpleUser.API.DTOs;

namespace SimpleUser.API.Validators
{
    public class SignUpValidator : AbstractValidator<SignUpDto>
    {
        public SignUpValidator()
        {
            RuleFor(x => x.FirstName)
              .NotNull()
              .WithMessage("FirstName cannot be empty");
            RuleFor(x => x.LastName)
              .NotNull()
              .WithMessage("LastName cannot be empty");
            RuleFor(x => x.Email)
              .NotNull()
              .WithMessage("Email cannot be empty")
              .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
              .WithMessage("Please enter a valid email address");
            RuleFor(x => x.Password)
              .NotNull()
              .WithMessage("Password cannot be empty");
        }
    }
}
