using FluentValidation;
using Cityton.Api.Data;
using Cityton.Api.Contracts.Requests;

namespace Cityton.Api.Contracts.Validators
{
    public class SignupRequestValidator : AbstractValidator<SignupRequest>
    {        public SignupRequestValidator(ApplicationDBContext appDBContext)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            // RuleFor(request => request.signupDTO).NotNull();
            // When(request => request.signupDTO != null,
            //     () => { RuleFor(request => request.signupDTO).SetValidator(new SignupDTOValidator(appDBContext)); });
        }
    }
}
