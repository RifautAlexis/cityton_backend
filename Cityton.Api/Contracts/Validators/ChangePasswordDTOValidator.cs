using FluentValidation;
using Cityton.Api.Validators.SharedValidators;
using Cityton.Api.Contracts.DTOs;

namespace Cityton.Api.Validators
{
    public class ChangePasswordDTOValidator : AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordDTOValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(cp => cp.OldPassword)
                .NotEmpty().WithMessage("OldPassword is empty");
            RuleFor(cp => cp.NewPassword)
                .NotEmpty().WithMessage("NewPassword is empty");
        }
    }
}
