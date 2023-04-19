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
            RuleFor(x => x).Custom((x, context) =>
            {
                if (_userService.IsUserAlreadyExistsByEmail(x.Email))
                {
                    context.AddFailure(nameof(x.Email), "Email already exist");
                }
            });
        }
    }
}
