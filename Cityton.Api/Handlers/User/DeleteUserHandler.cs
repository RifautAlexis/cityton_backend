using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Contracts.Requests;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Cityton.Api.Handlers
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
                .Include(u => u.GroupsSupervised)
                .FirstOrDefaultAsync();

            if (user == null) return new NotFoundObjectResult("No corresponding user was found");

            if (user.Role == Role.Member)
            {

                int belongingGroupId = user.ParticipantGroups
                    .Where(pg => pg.Status == Status.Accepted)
                    .Select(pg => pg.BelongingGroupId)
                    .FirstOrDefault();

                if (belongingGroupId > 0)
                {

                    bool isCreator = user.ParticipantGroups.Any(pg => pg.IsCreator);
                    if (isCreator)
                    {
                        Group groupToRemove = await _appDBContext.Groups
                        .Where(g => g.Id == belongingGroupId)
                        .Include(g => g.Members)
                        .Include(g => g.Discussion)
                            .ThenInclude(d => d.UsersInDiscussion)
                        .Include(d => d.Discussion)
                            .ThenInclude(d => d.Messages)
                        .FirstOrDefaultAsync();

                        if (groupToRemove == null) return new BadRequestObjectResult("Group Id found but not the group");

                        groupToRemove.SupervisorId = null;
                        groupToRemove.Members.Clear();

                        groupToRemove.Discussion.UsersInDiscussion.Clear();
                        groupToRemove.Discussion.Messages.Clear();

                        _appDBContext.Discussions.Remove(groupToRemove.Discussion);

                        _appDBContext.Groups.Remove(groupToRemove);

                        await _appDBContext.SaveChangesAsync();
                    }
                }

            } else {
                user.GroupsSupervised.Clear();
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

            user.UsersInDiscussion.Clear();

            _appDBContext.Users.Remove(user);
            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(true);
        }
    }
}