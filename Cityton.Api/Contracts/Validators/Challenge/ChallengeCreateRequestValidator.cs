using FluentValidation;
using Cityton.Api.Data;
using Cityton.Api.Contracts.Requests.Challenge;

namespace Cityton.Api.Contracts.Validators.Challenge
{
    public class ChallengeCreateRequestValidator : AbstractValidator<CreateRequest>
    {
        private readonly ApplicationDBContext _appDBContext;

        public ChallengeCreateRequestValidator(ApplicationDBContext appDBContext)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(request => request.challengeCreateDTO).NotNull();
            When(request => request.challengeCreateDTO != null,
                () => { RuleFor(request => request.challengeCreateDTO).SetValidator(new challengeCreateDTOValidator(appDBContext)); });
        }
    }
}
