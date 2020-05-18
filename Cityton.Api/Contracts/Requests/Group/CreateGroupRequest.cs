using Cityton.Api.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests
{
    public class CreateGroupRequest
    {
        [FromBody]
        public CreateGroupDTO createGroupDTO { get; set; }
    }
}