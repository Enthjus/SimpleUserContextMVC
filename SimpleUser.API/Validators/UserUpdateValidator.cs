using FluentValidation;
using SimpleUser.API.DTOs;

namespace SimpleUser.API.Validators
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();
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
            RuleFor(x => x.UserDetailDto).SetValidator(new UserDetailValidator());
        }
    }
}
