using FluentValidation;
using SimpleUser.API.DTOs;
using SimpleUser.API.Services;
using SimpleUser.MVC.Validators;

namespace SimpleUser.API.Validators
{
    public class CustomerCreateValidator : AbstractValidator<CustomerCreateDto>
    {
        private readonly ICustomerService _customerService;
        public CustomerCreateValidator(ICustomerService customerService) 
        { 
            _customerService = customerService;
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
            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("Password cannot be empty")
                .MaximumLength(50)
                .WithMessage("Password should no more than 50 character");
            RuleFor(x => x.CustomerDetailDto).SetValidator(new CustomerDetailValidator());
            RuleFor(x => x).Custom((x, context) =>
            {
                if (_customerService.IsCustomerAlreadyExistsByEmail(x.Email))
                {
                    context.AddFailure(nameof(x.Email), "Email already exist");
                }
            });
        }
    }
}
