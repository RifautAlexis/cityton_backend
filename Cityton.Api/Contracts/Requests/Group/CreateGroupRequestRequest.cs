using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests
{
    public class CreateGroupRequestRequest
    {
        [FromBody]
        public int GroupId { get; set; }
    }
}