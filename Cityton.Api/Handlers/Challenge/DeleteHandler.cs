using System;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Contracts.Requests.Challenge;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Cityton.Api.Handlers.Authentication
{
    public class DeleteHandler : IHandler<DeleteRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public DeleteHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(DeleteRequest request)
        {
            Challenge challenge = await _appDBContext.Challenges.Where(c => c.Id == request.Id).FirstOrDefaultAsync();

            if (challenge == null) { return new NotFoundObjectResult("No corresponding challenge was found"); }

            _appDBContext.Challenges.Remove(challenge);

            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(true);
        }
    }
}