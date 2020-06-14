using FluentValidation;
using Cityton.Api.Contracts.Requests;

namespace Cityton.Api.Contracts.Validators
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {

        public ChangePasswordRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(request => request.ChangePasswordDTO).NotNull();
            When(request => request.ChangePasswordDTO != null,
                () => { RuleFor(request => request.ChangePasswordDTO).SetValidator(new ChangePasswordDTOValidator()); });
        }
    }
}
