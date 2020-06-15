using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests
{
    public class ChangeRoleRequest : DefaultKeyedRequest
    {
        [FromBody]
        public int RoleId { get; set; }
    }
}