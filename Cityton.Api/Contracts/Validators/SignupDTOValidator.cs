using FluentValidation;
using Cityton.Api.Validators.SharedValidators;
using Cityton.Api.Contracts.DTOs;
using Cityton.Api.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cityton.Api.Validators
{
    public class SignupDTOValidator : AbstractValidator<SignupDTO>
    {
        private readonly ApplicationDBContext _appDBContext;

        public SignupDTOValidator(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;

            validator();
        }

        private void validator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(user => user.Username)
                .UsernameValidation()
                .MustAsync(async (username, cancellation) => !(await this.ExistUsername(username)));
            RuleFor(user => user.Email)
                .EmailValidation()
                .MustAsync(async (email, cancellation) => !(await this.ExistEmail(email)));
            RuleFor(user => user.Password).PasswordValidation();
        }

        private async Task<bool> ExistUsername(string username)
        {
            return await _appDBContext.Users.Where(user => user.Username == username).FirstOrDefaultAsync() != null;
        }

        private async Task<bool> ExistEmail(string email)
        {
            return await _appDBContext.Users.Where(user => user.Email == email).FirstOrDefaultAsync() != null;
        }
    }
}
