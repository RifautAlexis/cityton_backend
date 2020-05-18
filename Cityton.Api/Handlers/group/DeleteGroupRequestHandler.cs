using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Contracts.Requests;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Cityton.Api.Handlers
{
    public class DeleteGroupRequestHandler : IHandler<DeleteGroupRequestRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public DeleteGroupRequestHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(DeleteGroupRequestRequest request)
        {
            ParticipantGroup membershipToRemove = await _appDBContext.ParticipantGroups
                .Where(pg => pg.Id == request.Id && pg.Status == Status.Waiting)
                .FirstOrDefaultAsync();

            if (membershipToRemove == null) { return new NotFoundObjectResult("No corresponding request was found"); }

            _appDBContext.ParticipantGroups.Remove(membershipToRemove);

            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(true);
        }
    }
}