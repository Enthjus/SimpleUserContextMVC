using FluentValidation;
using SimpleUserContextMVC.DTOs;

namespace SimpleUserContextMVC.Validators
{
    public class UserDetailValidator : AbstractValidator<UserDetailDto>
    {
        public UserDetailValidator()
        {
            RuleFor(x => x.FirstName).NotNull().MaximumLength(50).WithMessage("First name should no more than 50 character");
            RuleFor(x => x.LastName).NotNull().MaximumLength(50).WithMessage("Last name should no more than 50 character");
            RuleFor(x => x.PhoneNumber).NotNull().Length(10).WithMessage("Phone number should no more than 50 character");
            RuleFor(x => x.Address).NotNull().MaximumLength(250).WithMessage("Address should no more than 50 character");
        }    
    }
}
