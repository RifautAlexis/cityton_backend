using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Contracts.Requests;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Cityton.Api.Handlers
{
    public class LeaveGroupHandler : IHandler<LeaveGroupRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LeaveGroupHandler(ApplicationDBContext appDBContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDBContext = appDBContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ObjectResult> Handle(LeaveGroupRequest request)
        {
            int currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value);

            ParticipantGroup membershipToRemove = await _appDBContext.ParticipantGroups
                .Where(pg => pg.BelongingGroupId == request.Id && pg.UserId == currentUserId && pg.Status == Status.Accepted)
                .FirstOrDefaultAsync();

            if (membershipToRemove == null) { return new NotFoundObjectResult("No corresponding members was found"); }

            _appDBContext.ParticipantGroups.Remove(membershipToRemove);

            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(true);
        }
    }
}