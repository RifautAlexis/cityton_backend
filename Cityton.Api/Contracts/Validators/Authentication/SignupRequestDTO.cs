using FluentValidation;
using Cityton.Api.Data;
using Cityton.Api.Contracts.Requests;

namespace Cityton.Api.Contracts.Validators
{
    public class SignupRequestValidator : AbstractValidator<SignupRequest>
    {        public SignupRequestValidator(ApplicationDBContext appDBContext)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(request => request.SignupDTO).NotNull();
            When(request => request.SignupDTO != null,
                () => { RuleFor(request => request.SignupDTO).SetValidator(new SignupDTOValidator(appDBContext)); });
        }
    }
}
