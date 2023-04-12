using FluentValidation;
using SimpleUser.MVC.DTOs;

namespace SimpleUser.MVC.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator() 
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Username).NotNull().MaximumLength(50).WithMessage("Username should no more than 50 character");
            RuleFor(x => x.Email).NotNull().Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Must have xxx@gmail.com form");
            RuleFor(x => x.Password).NotNull().MaximumLength(50).WithMessage("Password should no more than 50 character");
            RuleFor(x => x.UserDetailDto).SetValidator(new UserDetailValidator());
        }
    }
}
