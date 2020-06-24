using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Cityton.Api.Contracts.DTOs;

namespace Cityton.Api.Contracts.Requests
{
    public class AttributeChallengeToGroupRequest
    {
        [FromBody]
        public AttributeChallengeToAGroupDTO attributeChallengeToAGroupDTO { get; set; }
    }
}