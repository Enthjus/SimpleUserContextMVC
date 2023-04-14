using FluentValidation;
using SimpleUser.API.DTOs;

namespace SimpleUser.API.Validators
{
    public class UserDetailValidator : AbstractValidator<UserDetailDto>
    {
        public UserDetailValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .WithMessage("First name cannot be empty")
                .MaximumLength(50)
                .WithMessage("First name should no more than 50 character");
            RuleFor(x => x.LastName)
                .NotNull()
                .WithMessage("Last name cannot be empty")
                .MaximumLength(50)
                .WithMessage("Last name should no more than 50 character");
            RuleFor(x => x.PhoneNumber)
                .NotNull()
                .WithMessage("Phone number cannot be empty")
                .Length(10)
                .WithMessage("Phone number must have 10 character");
            RuleFor(x => x.Address)
                .NotNull()
                .WithMessage("Address cannot be empty")
                .MaximumLength(250)
                .WithMessage("Address should no more than 250 character");
        }    
    }
}
