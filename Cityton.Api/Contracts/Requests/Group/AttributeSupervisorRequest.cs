using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests
{
    public class AttributeSupervisorRequest : DefaultKeyedRequest
    {
        [FromBody]
        public int SupervisorId { get; set; }
    }
}