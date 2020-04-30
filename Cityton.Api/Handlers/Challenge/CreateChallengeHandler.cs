using System;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Contracts.Requests;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Cityton.Api.Handlers
{
    public class CreateChallengeHandler : IHandler<CreateChallengeRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateChallengeHandler(ApplicationDBContext appDBContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDBContext = appDBContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ObjectResult> Handle(CreateChallengeRequest request)
        {
            (string title, string statement) = request.challengeCreateDTO;
            int currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value);


            Challenge challenge = new Challenge
            {
                Title = title,
                Statement = statement,
                CreatedAt = DateTime.Now,
                AuthorId = currentUserId,
            };

            await _appDBContext.Challenges.AddAsync(challenge);
            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(true);
        }
    }
}