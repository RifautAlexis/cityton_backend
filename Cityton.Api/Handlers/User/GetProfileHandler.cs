using System.Linq;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cityton.Api.Contracts.Requests.User;
using Cityton.Api.Contracts.DTOs;

namespace Cityton.Api.Handlers.Authentication
{
    public class GetProfileHandler : IHandler<GetProfileRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public GetProfileHandler(ApplicationDBContext appDBContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(GetProfileRequest request)
        {
            User user = await _appDBContext.Users.Where(u => u.Id == request.Id).Include(u => u.ParticipantGroups).ThenInclude(pg => pg.BelongingGroup).FirstOrDefaultAsync();

            if (user == null) { return new NotFoundObjectResult("No user found"); }

            UserProfileDTO userProfile = new UserProfileDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Picture = user.Picture,
                Role = user.Role,
                GroupName = user.ParticipantGroups?.Where(pg => pg.Status == Status.Accepted).Select(pg => pg.BelongingGroup.Name).FirstOrDefault(),
            };

            return new OkObjectResult(userProfile);
        }
    }
}