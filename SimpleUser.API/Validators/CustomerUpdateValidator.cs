using FluentValidation;
using SimpleUser.API.DTOs;
using SimpleUser.API.Services;
using SimpleUser.MVC.Validators;

namespace SimpleUser.API.Validators
{
    public class CustomerUpdateValidator : AbstractValidator<CustomerUpdateDto>
    {
        private readonly ICustomerService _customerService;
        public CustomerUpdateValidator(ICustomerService customerService)
        {
            _customerService = customerService;
            RuleFor(x => x.Id)
                .NotNull();
            RuleFor(x => x.Customername)
                .NotNull()
                .WithMessage("Username cannot be empty")
                .MaximumLength(50)
                .WithMessage("Username should no more than 50 character");
            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("Email cannot be empty")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage("Please enter a valid email address");
            RuleFor(x => x.CustomerDetailDto).SetValidator(new CustomerDetailValidator());
            RuleFor(x => x).Custom((x, context) =>
            {
                if (_customerService.IsCustomerAlreadyExistsByEmail(x.Email, x.Id))
                {
                    context.AddFailure(nameof(x.Email), "Email already exist");
                }
            });
            RuleFor(x => x).Custom((x, context) =>
            {
                if (!string.IsNullOrEmpty(x.OldPassword))
                {
                    var Customer = _customerService.FindById(x.Id);
                    if (!BCrypt.Net.BCrypt.Verify(x.OldPassword, Customer.Password))
                    {
                        context.AddFailure(nameof(x.OldPassword), "Old password is not match");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(x.NewPassword))
                        {
                            context.AddFailure(nameof(x.NewPassword), "New password can not null");
                        }
                    }
                }
            });
        }
    }
}
