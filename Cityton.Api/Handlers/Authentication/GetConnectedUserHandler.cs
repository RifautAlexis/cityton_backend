using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Cityton.Api.Handlers.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cityton.Api.Contracts.Requests;

namespace Cityton.Api.Handlers
{
    public class GetConnectedUserHandler : IHandler<GetConnectedUserRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetConnectedUserHandler(ApplicationDBContext appDBContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDBContext = appDBContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ObjectResult> Handle(GetConnectedUserRequest request)
        {

            int currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value);

            User user = await _appDBContext.Users
                .Where(u => u.Id == currentUserId)
                .Include(u => u.ParticipantGroups)
                .FirstOrDefaultAsync();

            if (user == null) { return new NotFoundObjectResult("No user with this id is connected"); }

            return new OkObjectResult(user.ToDTO());
        }
    }
}