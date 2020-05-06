using System;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Contracts.Requests;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Cityton.Api.Handlers
{
    public class UndoChallengeHandler : IHandler<UndoChallengeRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public UndoChallengeHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(UndoChallengeRequest request)
        {
            ChallengeGiven challengeGiven = await _appDBContext.ChallengesGiven.Where(cg => cg.Id == request.Id).FirstOrDefaultAsync();

            if (challengeGiven == null) return new NotFoundObjectResult("No challengeGiven match the id given");

            challengeGiven.Status = StatusChallenge.InProgress;
            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(true);
        }
    }
}