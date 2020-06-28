using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Data;

namespace Cityton.Api.Contracts.Requests
{
    public class ChangeRoleRequest : DefaultKeyedRequest
    {
        [FromBody]
        public Role RoleId { get; set; }
    }
}