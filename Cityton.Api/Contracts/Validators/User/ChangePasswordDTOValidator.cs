using FluentValidation;
using Cityton.Api.Contracts.DTOs;

namespace Cityton.Api.Contracts.Validators
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
