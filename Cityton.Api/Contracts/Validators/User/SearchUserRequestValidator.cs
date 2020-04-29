using FluentValidation;
using Cityton.Api.Contracts.Requests.User;

namespace Cityton.Api.Contracts.Validators.User
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
