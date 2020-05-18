using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Contracts.Requests;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Cityton.Api.Handlers
{
    public class EditGroupNameHandler : IHandler<EditGroupNameRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public EditGroupNameHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(EditGroupNameRequest request)
        {
            Group groupToUpdate = await _appDBContext.Groups
                .Where(g => g.Id == request.Id)
                .FirstOrDefaultAsync();

            if (groupToUpdate == null) { return new NotFoundObjectResult("No corresponding group was found"); }

            groupToUpdate.Name = request.GroupName;

            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(true);
        }
    }
}