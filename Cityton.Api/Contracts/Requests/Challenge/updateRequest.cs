using Cityton.Api.Contracts.DTOs.Challenge;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests.Challenge
{
    public class UpdateRequest
    {
        [FromBody]
        public UpdateDTO challengeUpdateDTO { get; set; }
    }
}
