using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Contracts.Requests;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Cityton.Api.Handlers
{
    public class AcceptGroupRequestHandler : IHandler<AcceptGroupRequestRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public AcceptGroupRequestHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(AcceptGroupRequestRequest request)
        {
            ParticipantGroup membershipToUpdate = await _appDBContext.ParticipantGroups
                .Where(pg => pg.Id == request.Id && pg.Status == Status.Waiting)
                .FirstOrDefaultAsync();

            if (membershipToUpdate == null) { return new NotFoundObjectResult("No corresponding request was found"); }

            int maxGroupSize = await _appDBContext.Companies
                .Where(c => c.Id == 1)
                .Select(c => c.MaxGroupSize)
                .FirstOrDefaultAsync();

            int actualGroupSize = await _appDBContext.Groups
                .Where(g => g.Id == membershipToUpdate.BelongingGroupId)
                .Select(g => g.Members.Count)
                .FirstOrDefaultAsync();

            if (actualGroupSize < maxGroupSize)
            {
                membershipToUpdate.Status = Status.Accepted;

                await _appDBContext.SaveChangesAsync();

                List<ParticipantGroup> requestsToDelete = await _appDBContext.ParticipantGroups
                    .Where(pg => pg.UserId == membershipToUpdate.UserId && pg.Status == Status.Waiting)
                    .ToListAsync();

                _appDBContext.ParticipantGroups.RemoveRange(requestsToDelete);

                await _appDBContext.SaveChangesAsync();

                return new OkObjectResult(true);

            }
            else
            {
                return new BadRequestObjectResult("Group has already ready reach max size");
            }


        }
    }
}