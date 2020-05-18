using System;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Contracts.Requests;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Cityton.Api.Handlers
{
    public class CreateGroupHandler : IHandler<CreateGroupRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateGroupHandler(ApplicationDBContext appDBContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDBContext = appDBContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ObjectResult> Handle(CreateGroupRequest request)
        {
            string name = request.createGroupDTO.Name;
            int currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value);


            Group group = new Group
            {
                Name = name,
                CreatedAt = DateTime.Now,
            };

            await _appDBContext.Groups.AddAsync(group);
            await _appDBContext.SaveChangesAsync();

            ParticipantGroup participantGroups = new ParticipantGroup
            {
                IsCreator = true,
                Status = Status.Accepted,
                BelongingGroupId = group.Id,
                UserId = currentUserId
            };

            await _appDBContext.ParticipantGroups.AddAsync(participantGroups);
            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(true);
        }
    }
}