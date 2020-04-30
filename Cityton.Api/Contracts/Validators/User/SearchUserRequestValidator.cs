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
                .IsInEnum().WithMessage("Is not a Role");
        }
    }
}
