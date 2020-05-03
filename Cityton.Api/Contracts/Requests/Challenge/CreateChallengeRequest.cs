using Cityton.Api.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests
{
    public class CreateChallengeRequest
    {
        [FromBody]
        public CreateChallengeDTO challengeCreateDTO { get; set; }
    }
}
