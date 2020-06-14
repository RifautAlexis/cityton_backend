using FluentValidation;
using Cityton.Api.Data;
using Cityton.Api.Contracts.Requests;
using Cityton.Api.Contracts.Validators.SharedValidators;

namespace Cityton.Api.Contracts.Validators
{
    public class CreateGroupRequestRequestValidator : AbstractValidator<CreateGroupRequestRequest>
    {
        public CreateGroupRequestRequestValidator(ApplicationDBContext appDBContext)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(Requests => Requests.GroupId)
                .SetValidator(new IdValidator());
        }
    }
}
