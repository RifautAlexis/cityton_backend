using FluentValidation;
using Cityton.Api.Data;
using Cityton.Api.Contracts.Requests;

namespace Cityton.Api.Contracts.Validators
{
    public class AttributeChallengeToGroupRequestValidator : AbstractValidator<AttributeChallengeToGroupRequest>
    {
        public AttributeChallengeToGroupRequestValidator(ApplicationDBContext appDBContext)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(request => request.attributeChallengeToAGroupDTO).NotNull();
            When(request => request.attributeChallengeToAGroupDTO != null,
                () => { RuleFor(request => request.attributeChallengeToAGroupDTO).SetValidator(new AttributeChallengeToAGroupDTOValidator()); });
        }
    }
}
