using FluentValidation;
using Cityton.Api.Contracts.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Cityton.Api.Data;
using System.Linq;
using Cityton.Api.Contracts.Requests;

namespace Cityton.Api.Contracts.Validators
{
    public class CreateGroupRequestValidator : AbstractValidator<CreateGroupRequest>
    {
        public CreateGroupRequestValidator(ApplicationDBContext appDBContext)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(request => request.createGroupDTO).NotNull();
            When(request => request.createGroupDTO != null,
                () => { RuleFor(request => request.createGroupDTO).SetValidator(new CreateGroupDTOValidator(appDBContext)); });
        }
    }
}
