using FluentValidation;
using Cityton.Api.Data;
using Cityton.Api.Contracts.Requests;

namespace Cityton.Api.Contracts.Validators
{
    public class ChallengeCreateRequestValidator : AbstractValidator<CreateChallengeRequest>
    {
        public ChallengeCreateRequestValidator(ApplicationDBContext appDBContext)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(request => request.challengeCreateDTO).NotNull();
            When(request => request.challengeCreateDTO != null,
                () => { RuleFor(request => request.challengeCreateDTO).SetValidator(new challengeCreateDTOValidator(appDBContext)); });
        }
    }
}
