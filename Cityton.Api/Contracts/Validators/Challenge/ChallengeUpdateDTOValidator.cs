using FluentValidation;
using Cityton.Api.Contracts.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Cityton.Api.Data;
using System.Linq;
using Cityton.Api.Contracts.Validators.SharedValidators;

namespace Cityton.Api.Contracts.Validators
{
    public class ChallengeUpdateDTOValidator : AbstractValidator<UpdateChallengeDTO>
    {
        private readonly ApplicationDBContext _appDBContext;

        public ChallengeUpdateDTOValidator(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(cc => cc.ChallengeId)
                .SetValidator(new IdValidator());
            RuleFor(cc => cc.Title)
                .NotEmpty().WithMessage("Title is empty")
                .Length(3, 50).WithMessage("Have to contains between 3 to 50 characters !")
                .MustAsync(async (title, cancellation) => !(await this.ExistTitle(title)))
                .WithMessage("{PropertyValue} is already taken !");
            RuleFor(cc => cc.Statement)
                .NotEmpty().WithMessage("Statement is empty")
                .Length(10, 100).WithMessage("Have to contains between 10 to 100 characters !");
        }
        
        private async Task<bool> ExistTitle(string title)
        {
            return await _appDBContext.Challenges.Where(c => c.Title == title).FirstOrDefaultAsync() != null;
        }
    }
}
