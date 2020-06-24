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
    public class AttributeChallengeToGroupHandler : IHandler<AttributeChallengeToGroupRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public AttributeChallengeToGroupHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(AttributeChallengeToGroupRequest request)
        {

            (int threadId, List<int> challengeIds) = request.attributeChallengeToAGroupDTO;

            int groupId = await _appDBContext.Groups
                            .Where(g => g.DiscussionId == threadId)
                            .Select(g => g.Id)
                            .FirstOrDefaultAsync();

            if (groupId == 0) return new BadRequestObjectResult("This discussion id do not correspond with any group");

            List<ChallengeGiven> challengesCiven = await _appDBContext.ChallengesGiven
                .Where(cg => cg.ChallengedGroup.DiscussionId == threadId && challengeIds.Any(ci => ci == cg.ChallengeId))
                .ToListAsync();

            if (challengesCiven.Count != 0) return new BadRequestObjectResult("One or more challenges selected had already given");

            foreach (var challengeId in challengeIds)
            {
                ChallengeGiven newChallengeGiven = new ChallengeGiven
                {
                    Status = StatusChallenge.InProgress,
                    ChallengeId = challengeId,
                    ChallengedGroupId = groupId
                };

                await _appDBContext.ChallengesGiven.AddAsync(newChallengeGiven);
            }

            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(true);
        }
    }
}