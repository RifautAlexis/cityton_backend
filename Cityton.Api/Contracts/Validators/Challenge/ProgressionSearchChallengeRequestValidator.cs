using FluentValidation;
using Cityton.Api.Data;
using Cityton.Api.Contracts.Requests;
using Cityton.Api.Contracts.Validators.SharedValidators;

namespace Cityton.Api.Contracts.Validators
{
    public class ProgressionSearchChallengeRequestValidator : AbstractValidator<ProgressionSearchChallengeRequest>
    {
        public ProgressionSearchChallengeRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(request => request.ThreadId)
                .SetValidator(new IdValidator());
        }
    }
}
