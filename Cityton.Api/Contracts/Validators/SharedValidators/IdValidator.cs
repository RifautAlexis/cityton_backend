using FluentValidation;

namespace Cityton.Api.Contracts.Validators.SharedValidators
{
    public class IdValidator : AbstractValidator<int>
    {
        public IdValidator()
        {
            RuleFor(id => id)
                .NotEmpty()
                .WithMessage("Id can not be equals to 0");
        }
    }
}