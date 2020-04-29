using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Contracts.Requests.Challenge;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Cityton.Api.Handlers.Authentication
{
    public class DeleteChallengeHandler : IHandler<DeleteChallengeRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public DeleteChallengeHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(DeleteChallengeRequest request)
        {
            Challenge challenge = await _appDBContext.Challenges.Where(c => c.Id == request.Id).FirstOrDefaultAsync();

            if (challenge == null) { return new NotFoundObjectResult("No corresponding challenge was found"); }

            challenge.Achievements.Clear();
            challenge.ChallengeGivens.Clear();

            _appDBContext.Challenges.Remove(challenge);

            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(true);
        }
    }
}