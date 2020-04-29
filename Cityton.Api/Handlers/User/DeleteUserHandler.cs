using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Contracts.Requests.User;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Cityton.Api.Handlers.Authentication
{
    public class DeleteUserHandler : IHandler<DeleteUserRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public DeleteUserHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(DeleteUserRequest request)
        {
            User user = await _appDBContext.Users
                .Where(u => u.Id == request.Id)
                .Include(u => u.ParticipantGroups)
                    .ThenInclude(pg => pg.BelongingGroup)
                .Include(u => u.Challenges)
                .Include(u => u.Achievements)
                .Include(u => u.UsersInDiscussion)
                .Include(u => u.MessagesWriten)
                .FirstOrDefaultAsync();

            if (user == null) { return new NotFoundObjectResult("No corresponding user was found"); }

            int groupId = user.ParticipantGroups
                .Where(pg => pg.IsCreator == true)
                .Select(pg => pg.BelongingGroupId)
                .FirstOrDefault();

            if (groupId > 0)
            {

                Group groupToRemove = await _appDBContext.Groups
                    .Where(g => g.Id == groupId)
                    .Include(g => g.Members)
                    .Include(g => g.Discussion)
                        .ThenInclude(d => d.UsersInDiscussion)
                    .Include(d => d.Discussion)
                        .ThenInclude(d => d.Messages)
                    .FirstOrDefaultAsync();


                if (groupToRemove != null)
                {
                    groupToRemove.Members.Clear();

                    groupToRemove.Discussion.UsersInDiscussion.Clear();
                    groupToRemove.Discussion.Messages.Clear();

                    _appDBContext.Discussions.Remove(groupToRemove.Discussion);

                    _appDBContext.Groups.Remove(groupToRemove);
                }
            }

            foreach (var messageWriten in user.MessagesWriten)
            {
                messageWriten.AuthorId = null;
            }

            foreach (var challengesCreated in user.Challenges)
            {
                challengesCreated.AuthorId = null;
            }

            foreach (var achievement in user.Achievements)
            {
                achievement.WinnerId = null;
            }

            _appDBContext.Users.Remove(user);

            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(true);
        }
    }
}