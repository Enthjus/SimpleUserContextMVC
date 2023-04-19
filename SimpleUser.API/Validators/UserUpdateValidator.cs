using FluentValidation;
using SimpleUser.API.DTOs;
using SimpleUser.API.Services;

namespace SimpleUser.API.Validators
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
    {
        private readonly IUserService _userService;
        public UserUpdateValidator(IUserService userService)
        {
            _userService = userService;
            RuleFor(x => x).Custom((x, context) =>
            {
                if (_userService.IsUserAlreadyExistsByEmail(x.Email, x.Id))
                {
                    context.AddFailure(nameof(x.Email), "Email already exist");
                }
            });
            RuleFor(x => x).Custom((x, context) =>
            {
                if (!string.IsNullOrEmpty(x.NewPassword) & !string.IsNullOrEmpty(x.OldPassword))
                {
                    var user = userService.FindById(x.Id);
                    if (!BCrypt.Net.BCrypt.Verify(x.OldPassword, user.Password))
                    {
                        context.AddFailure(nameof(x.OldPassword), "Old password is not match");
                    }

                }
            });
        }
    }
}
