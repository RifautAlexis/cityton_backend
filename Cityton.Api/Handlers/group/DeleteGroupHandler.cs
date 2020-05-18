using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Contracts.Requests;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Cityton.Api.Handlers
{
    public class DeleteGroupHandler : IHandler<DeleteGroupRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public DeleteGroupHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(DeleteGroupRequest request)
        {
            Group groupToRemove = await _appDBContext.Groups.Where(g => g.Id == request.Id)
                .Include(g => g.Members)
                .Include(g => g.Discussion)
                    .ThenInclude(d => d.UsersInDiscussion)
                .Include(d => d.Discussion)
                    .ThenInclude(d => d.Messages)
                .FirstOrDefaultAsync();

            if (groupToRemove == null) { return new NotFoundObjectResult("No corresponding group was found"); }

            groupToRemove.Members.Clear();

            groupToRemove.Discussion.UsersInDiscussion.Clear();
            groupToRemove.Discussion.Messages.Clear();

            _appDBContext.Discussions.Remove(groupToRemove.Discussion);

            _appDBContext.Groups.Remove(groupToRemove);

            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(true);
        }
    }
}