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
    public class UpdateChallengeHandler : IHandler<UpdateChallengeRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateChallengeHandler(ApplicationDBContext appDBContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDBContext = appDBContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ObjectResult> Handle(UpdateChallengeRequest request)
        {
            (int challengeId, string title, string statement) = request.challengeUpdateDTO;
            Challenge challenge = await _appDBContext.Challenges.Where(c => c.Id == challengeId).FirstOrDefaultAsync();

            if (challenge == null) { return new NotFoundObjectResult("No corresponding challenge was found"); }

            challenge.Title = title;
            challenge.Statement = statement;

            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(true);
        }
    }
}