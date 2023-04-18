using FluentValidation;
using SimpleUser.MVC.DTOs;

namespace SimpleUser.MVC.Validators
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
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.NewPassword != x.ConfirmNewPassword)
                {
                    context.AddFailure(nameof(x.ConfirmNewPassword), "Confirm password must match with password");
                }
            });
            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("Email cannot be empty")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage("Please enter a valid email address");
            RuleFor(x => x.UserDetailDto).SetValidator(new UserDetailValidator());
        }
    }
}
