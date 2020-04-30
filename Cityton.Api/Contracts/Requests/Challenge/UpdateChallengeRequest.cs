using Cityton.Api.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests
{
    public class UpdateChallengeRequest
    {
        [FromBody]
        public UpdateDTO challengeUpdateDTO { get; set; }
    }
}
