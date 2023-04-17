using FluentValidation;
using SimpleUser.API.DTOs;
using SimpleUser.API.Services;

namespace SimpleUser.API.Validators
{
    public class UserCreateValidator : AbstractValidator<UserCreateDto>
    {
        private readonly IUserService _userService;
        public UserCreateValidator(IUserService userService) 
        { 
            _userService = userService;
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
                .WithMessage("Email cannot be empty")
                .MaximumLength(50)
                .WithMessage("Password should no more than 50 character");
            RuleFor(x => x.ConfirmPassword)
                .NotNull()
                .WithMessage("Email cannot be empty");
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure(nameof(x.ConfirmPassword), "Confirm password must match with password");
                }
            });
            RuleFor(x => x).Custom((x, context) =>
            {
                if (_userService.IsUserAlreadyExistsByEmail(x.Email))
                {
                    context.AddFailure(nameof(x.Email), "Email already exist");
                }
            });
            RuleFor(x => x.UserDetailDto).SetValidator(new UserDetailValidator());
        }
    }
}
