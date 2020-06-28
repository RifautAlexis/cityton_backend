using FluentValidation;
using Cityton.Api.Data;
using Cityton.Api.Contracts.Requests;
using Cityton.Api.Contracts.Validators.SharedValidators;

namespace Cityton.Api.Contracts.Validators
{
    public class ChangeRoleRequestValidator : AbstractValidator<ChangeRoleRequest>
    {
        public ChangeRoleRequestValidator(ApplicationDBContext appDBContext)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(request => request.Id)
                .SetValidator(new IdValidator());
            RuleFor(request => request.RoleId)
                .IsInEnum().WithMessage("Is not a valid Role");

        }
    }
}
