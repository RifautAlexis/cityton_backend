using Cityton.Api.Contracts.DTOs.Challenge;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests.Challenge
{
    public class CreateChallengeRequest
    {
        [FromBody]
        public CreateDTO challengeCreateDTO { get; set; }
    }
}
