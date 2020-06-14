using FluentValidation;
using Cityton.Api.Contracts.DTOs;
using Cityton.Api.Validators.SharedValidators;

namespace Cityton.Api.Contracts.Validators
{
    public class ChangePasswordDTOValidator : AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordDTOValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(cp => cp.OldPassword)
                .NotEmpty().WithMessage("OldPassword is empty");
            RuleFor(cp => cp.NewPassword).PasswordValidation();
        }
    }
}
