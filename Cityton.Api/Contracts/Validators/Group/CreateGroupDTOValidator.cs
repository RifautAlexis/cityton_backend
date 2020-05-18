using FluentValidation;
using Cityton.Api.Contracts.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Cityton.Api.Data;
using System.Linq;
using Cityton.Api.Contracts.Requests;

namespace Cityton.Api.Contracts.Validators
{

    public class CreateGroupDTOValidator : AbstractValidator<CreateGroupDTO>
    {
        private readonly ApplicationDBContext _appDBContext;

        public CreateGroupDTOValidator(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(cg => cg.Name)
                .NotEmpty().WithMessage("Name is empty")
                .Length(3, 50).WithMessage("Have to contains between 3 to 50 characters !")
                .MustAsync(async (name, cancellation) => !(await this.ExistName(name)))
                .WithMessage("{PropertyValue} is already taken !");
        }

        private async Task<bool> ExistName(string name)
        {
            return await _appDBContext.Groups.Where(c => c.Name == name).FirstOrDefaultAsync() != null;
        }
    }
}
