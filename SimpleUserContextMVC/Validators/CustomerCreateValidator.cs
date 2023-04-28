using FluentValidation;
using SimpleUser.MVC.DTOs;

namespace SimpleUser.MVC.Validators
{
    public class CustomerCreateValidator : AbstractValidator<CustomerCreateDto>
    {
        public CustomerCreateValidator()
        {
            RuleFor(x => x.Username)
               .NotNull()
               .WithMessage("Username cannot be empty")
               .MaximumLength(50)
               .WithMessage("Username should no more than 50 character");
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
            RuleFor(x => x.ConfirmPassword)
                .NotNull()
                .WithMessage("Confirm Password cannot be empty");
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure(nameof(x.ConfirmPassword), "Confirm password must match with password");
                }
            });
            RuleFor(x => x.CustomerDetailDto).SetValidator(new CustomerDetailValidator());
        }
    }
}
