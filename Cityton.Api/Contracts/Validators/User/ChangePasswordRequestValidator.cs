using FluentValidation;
using Cityton.Api.Contracts.Requests;

namespace Cityton.Api.Contracts.Validators
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {

        public ChangePasswordRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(request => request.changePasswordDTO).NotNull();
            When(request => request.changePasswordDTO != null,
                () => { RuleFor(request => request.changePasswordDTO).SetValidator(new ChangePasswordDTOValidator()); });
        }
    }
}
