using System;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Contracts.Requests;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Cityton.Api.Contracts.DTOs;
using System.Collections.Generic;
using Cityton.Api.Handlers.Mappers;

namespace Cityton.Api.Handlers
{
    public class CreateGroupRequestHandler : IHandler<CreateGroupRequestRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateGroupRequestHandler(ApplicationDBContext appDBContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDBContext = appDBContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ObjectResult> Handle(CreateGroupRequestRequest request)
        {
            int currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value);

            Group group = await _appDBContext.Groups
                .Where(g => g.Id == request.GroupId)
                .FirstOrDefaultAsync();

            if (group == null) { return new NotFoundObjectResult("No corresponding group was found"); }

            int maxGroupSize = await _appDBContext.Companies
                .Where(c => c.Id == 1)
                .Select(c => c.MaxGroupSize)
                .FirstOrDefaultAsync();

            if (maxGroupSize == group.Members.Count) { return new BadRequestObjectResult("Group is full"); }

            ParticipantGroup participantGroup = new ParticipantGroup
            {
                IsCreator = false,
                Status = Status.Waiting,
                CreatedAt = DateTime.Now,
            };
            
            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(true);
        }
    }
}