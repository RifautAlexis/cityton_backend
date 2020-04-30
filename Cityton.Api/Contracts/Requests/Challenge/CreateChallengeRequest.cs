using Cityton.Api.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests
{
    public class CreateChallengeRequest
    {
        [FromBody]
        public CreateDTO challengeCreateDTO { get; set; }
    }
}
