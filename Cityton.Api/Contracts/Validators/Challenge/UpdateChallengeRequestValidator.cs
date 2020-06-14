using FluentValidation;
using Cityton.Api.Contracts.Requests;
using Cityton.Api.Data;

namespace Cityton.Api.Contracts.Validators
{
    public class UpdateChallengeRequestValidator : AbstractValidator<UpdateChallengeRequest>
    {
        private readonly ApplicationDBContext _appDBContext;

        public UpdateChallengeRequestValidator(ApplicationDBContext appDBContext)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(request => request.challengeUpdateDTO).NotNull();
            When(request => request.challengeUpdateDTO != null,
                () => { RuleFor(request => request.challengeUpdateDTO).SetValidator(new ChallengeUpdateDTOValidator(appDBContext)); });
        }
    }
}
