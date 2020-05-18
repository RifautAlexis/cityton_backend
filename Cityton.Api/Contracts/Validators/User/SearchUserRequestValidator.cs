using FluentValidation;
using Cityton.Api.Contracts.Requests;

namespace Cityton.Api.Contracts.Validators
{
    public class SearchUserRequestValidator : AbstractValidator<SearchUserRequest>
    {
        public SearchUserRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(sur => sur.SelectedRole)
                .NotNull().WithMessage("SelectedFilter is null")
                .IsInEnum().WithMessage("Is not a Role");
        }
    }
}
