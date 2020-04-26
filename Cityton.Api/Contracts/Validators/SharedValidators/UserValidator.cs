using FluentValidation;

namespace Cityton.Api.Validators.SharedValidators
{
    public static class UserValidator
    {
        public static IRuleBuilderOptions<T, string> EmailValidation<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .NotEmpty().WithMessage("Can't be empty !")
                .EmailAddress().WithMessage("Wrong email format !");
        }

        public static IRuleBuilderOptions<T, string> PasswordValidation<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .NotEmpty().WithMessage("Can't be empty !")
                .MinimumLength(3).WithMessage("Require at least 3 characters !");
        }

        public static IRuleBuilderOptions<T, string> UsernameValidation<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .NotEmpty()
                .MinimumLength(3).WithMessage("Require at least 3 characters !");
        }
    }
}
