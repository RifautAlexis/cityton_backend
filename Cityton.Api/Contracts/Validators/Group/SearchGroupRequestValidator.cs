using FluentValidation;
using Cityton.Api.Contracts.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Cityton.Api.Data;
using System.Linq;
using Cityton.Api.Contracts.Requests;

namespace Cityton.Api.Contracts.Validators
{
    public class SearchGroupRequestValidator : AbstractValidator<SearchGroupRequest>
    {
        private readonly ApplicationDBContext _appDBContext;
        
        public SearchGroupRequestValidator(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(request => request.SelectedFilter)
                .NotNull().WithMessage("SelectedFilter is null")
                .IsInEnum().WithMessage("Is not a valid filter");
        }
    }
}
