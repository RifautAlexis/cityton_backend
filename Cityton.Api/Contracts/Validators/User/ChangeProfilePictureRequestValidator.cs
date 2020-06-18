using FluentValidation;
using Cityton.Api.Data;
using Cityton.Api.Contracts.Requests;
using Cityton.Api.Contracts.Validators.File;

namespace Cityton.Api.Contracts.Validators
{
    public class ChangeProfilePictureRequestValidator : AbstractValidator<ChangeProfilePictureRequest>
    {
        public ChangeProfilePictureRequestValidator(ApplicationDBContext appDBContext)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(request => request.File)
                .NotNull().WithMessage("File is null")
                .SetValidator(new ImageValidator());
        }
    }
}
