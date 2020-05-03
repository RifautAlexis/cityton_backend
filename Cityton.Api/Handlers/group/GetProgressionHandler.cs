using System;
using System.Linq;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cityton.Api.Contracts.Requests;
using Cityton.Api.Contracts.DTOs;
using Cityton.Api.Handlers.Mappers;
using System.Collections.Generic;
using System.Security.Claims;

namespace Cityton.Api.Handlers
{
    public class GetProgressionHandler : IHandler<GetProgressionRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public GetProgressionHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(GetProgressionRequest request)
        {
            Discussion thread = await _appDBContext.Discussions.Where(d => d.Id == request.Id)
                .Include(d => d.Group)
                    .ThenInclude(g => g.ChallengesGiven)
                        .ThenInclude(cg => cg.Challenge)
                .FirstOrDefaultAsync();

            if (thread == null) { return new NotFoundObjectResult("No discussion with this id exists"); }
            if (thread.GroupId == null) { return new BadRequestObjectResult("This thread is not a group discussion"); }

            List<ChallengeMinimalDTO> succeedChallenges = thread.Group.ChallengesGiven.Where(cg => cg.Status == StatusChallenge.Validated).Select(cg => cg.Challenge).ToList().ToChallengeMinimalDTO();
            double totalChallenges = await _appDBContext.Challenges.CountAsync();

            GroupProgressionDTO progression = new GroupProgressionDTO{
                GroupId = thread.GroupId ?? 0,
                Progression = (succeedChallenges.Count / totalChallenges) * 100.0,
                InProgress = thread.Group.ChallengesGiven.Where(cg => cg.Status == StatusChallenge.InProgress).Select(cg => cg.Challenge).ToList().ToChallengeMinimalDTO(),
                Succeed = succeedChallenges,
                Failed = thread.Group.ChallengesGiven.Where(cg => cg.Status == StatusChallenge.Rejected).Select(cg => cg.Challenge).ToList().ToChallengeMinimalDTO(),
            };

            return new OkObjectResult(progression);
        }
    }
}