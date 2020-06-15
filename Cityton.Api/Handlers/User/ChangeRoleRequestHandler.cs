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
    public class ChangeRoleRequestHandler : IHandler<ChangeRoleRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public ChangeRoleRequestHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(ChangeRoleRequest request)
        {
            User user = await _appDBContext.Users
                .Where(u => u.Id == request.Id)
                .Include(u => u.GroupsSupervised)
                    .ThenInclude(g => g.Discussion)
                .Include(u => u.UsersInDiscussion)
                .FirstOrDefaultAsync();

            if (user == null) { return new NotFoundObjectResult("No corresponding user was found"); }

            if ((user.Role == Role.Admin || user.Role == Role.Checker) && (Role)request.RoleId == Role.Member)
            {
                List<UserInDiscussion> usersInDiscussion = user.UsersInDiscussion.Where(uid => user.GroupsSupervised.Any(g => g.DiscussionId == uid.Discussion.Id)).ToList();
                _appDBContext.UsersInDiscussion.RemoveRange(usersInDiscussion);

                _appDBContext.RemoveRange(user.GroupsSupervised);

                await _appDBContext.SaveChangesAsync();

            }

            user.Role = (Role)request.RoleId;

            return new OkObjectResult(true);
        }
    }
}