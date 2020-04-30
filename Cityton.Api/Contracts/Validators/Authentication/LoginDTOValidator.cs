using FluentValidation;
using Cityton.Api.Validators.SharedValidators;
using Cityton.Api.Contracts.DTOs;

namespace Cityton.Api.Contracts.Validators
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(user => user.Email).EmailValidation();
            RuleFor(user => user.Password).PasswordValidation();
        }
    }
}
