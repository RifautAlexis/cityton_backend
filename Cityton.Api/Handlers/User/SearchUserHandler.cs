using System;
using System.Linq;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cityton.Api.Contracts.Requests;
using Cityton.Api.Contracts.DTOs;
using System.Collections.Generic;
using Cityton.Api.Contracts.Mappers;

namespace Cityton.Api.Handlers
{
    public class SearchUserHandler : IHandler<SearchUserRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public SearchUserHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(SearchUserRequest request)
        {
            StringComparison comparison = StringComparison.OrdinalIgnoreCase;

            List<UserProfileDTO> userProfileDTO = await _appDBContext.Users
                .Where(u => (request.SelectedRole == null || u.Role == request.SelectedRole) && (string.IsNullOrEmpty(request.SearchText) || u.Username.Contains(request.SearchText, comparison)))
                .OrderByDescending(u => u.Role)
                .Include(u => u.ParticipantGroups)
                    .ThenInclude(pg => pg.BelongingGroup)
                .Select(u => u.ToUserProfile())
                .ToListAsync();

            return new OkObjectResult(userProfileDTO);
        }
    }
}