using FluentValidation;
using Cityton.Api.Contracts.DTOs;
using Cityton.Api.Contracts.Validators.SharedValidators;

namespace Cityton.Api.Contracts.Validators
{
    public class AttributeChallengeToAGroupDTOValidator : AbstractValidator<AttributeChallengeToAGroupDTO>
    {
        public AttributeChallengeToAGroupDTOValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(actag => actag.ThreadId)
                .SetValidator(new IdValidator());
            RuleFor(actag => actag.ChallengeIds.Count)
                .GreaterThan(0).WithMessage("None challenge ids");
            RuleForEach(actag => actag.ChallengeIds)
                .SetValidator(new IdValidator());
        }
    }
}
