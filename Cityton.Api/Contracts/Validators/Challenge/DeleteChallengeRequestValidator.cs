using FluentValidation;
using Cityton.Api.Data;
using Cityton.Api.Contracts.Requests;
using Cityton.Api.Contracts.Validators.SharedValidators;

namespace Cityton.Api.Contracts.Validators
{
    public class DeleteChallengeRequestValidator : AbstractValidator<DeleteChallengeRequest>
    {
        public DeleteChallengeRequestValidator(ApplicationDBContext appDBContext)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(request => request.Id)
                .SetValidator(new IdValidator());
        }
    }
}
