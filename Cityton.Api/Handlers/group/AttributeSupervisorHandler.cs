using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Contracts.Requests;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cityton.Api.Handlers
{
    public class AttributeSupervisorHandler : IHandler<AttributeSupervisorRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public AttributeSupervisorHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(AttributeSupervisorRequest request)
        {
            Group groupToUpdate = await _appDBContext.Groups
                .Where(g => g.Id == request.Id)
                .FirstOrDefaultAsync();

            if (groupToUpdate == null) { return new NotFoundObjectResult("No corresponding group was found"); }

            if (groupToUpdate.SupervisorId != null)
            {
                UserInDiscussion userInDiscussionToDelete = await _appDBContext.UsersInDiscussion
                    .Where(uid => uid.ParticipantId == groupToUpdate.SupervisorId)
                    .FirstOrDefaultAsync();

                _appDBContext.UsersInDiscussion.Remove(userInDiscussionToDelete);
            }

                groupToUpdate.SupervisorId = request.SupervisorId;

                await _appDBContext.SaveChangesAsync();

                UserInDiscussion userInDiscussion = new UserInDiscussion
                {
                    JoinedAt = DateTime.Now,
                    DiscussionId = groupToUpdate.Id,
                    ParticipantId = request.SupervisorId
                };

                await _appDBContext.UsersInDiscussion.AddAsync(userInDiscussion);
                await _appDBContext.SaveChangesAsync();


            return new OkObjectResult(true);
        }
    }
}