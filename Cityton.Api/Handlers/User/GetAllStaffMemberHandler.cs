using System.Linq;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cityton.Api.Contracts.Requests;
using Cityton.Api.Contracts.DTOs;
using Cityton.Api.Contracts.Mappers;
using System.Collections.Generic;

namespace Cityton.Api.Handlers
{
    public class GetAllStaffMemberHandler : IHandler<GetAllStaffMemberRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public GetAllStaffMemberHandler(ApplicationDBContext appDBContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(GetAllStaffMemberRequest request)
        {
            List<User> users = await _appDBContext.Users.Where(u => u.Role == Role.Admin || u.Role == Role.Checker).ToListAsync();

            if (users.Count == 0) { return new NotFoundObjectResult("There is no staff members found"); }

            List<UserMinimalDTO> staffMembers = users.ToUserMinimalDTO();

            return new OkObjectResult(staffMembers);
        }
    }
}