using FluentValidation;
using Cityton.Api.Contracts.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Cityton.Api.Data;
using System.Linq;

namespace Cityton.Api.Contracts.Validators
{
    public class CreateMessageDTOValidator : AbstractValidator<CreateMessageDTO>
    {
        private readonly ApplicationDBContext _appDBContext;

        public CreateMessageDTOValidator(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(cm => cm.Message)
                .NotEmpty()
                .When(cm => string.IsNullOrEmpty(cm.ImageUrl))
                .WithMessage("Message and ImageUrl are null or empty");
        }
    }
}
