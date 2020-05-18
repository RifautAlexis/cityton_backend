using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests
{
    public class EditGroupNameRequest : DefaultKeyedRequest
    {
        [FromBody]
        public string GroupName { get; set; }
    }
}