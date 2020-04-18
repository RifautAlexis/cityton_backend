using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests
{
    public class DefaultKeyedRequest
    {
        [FromRoute]
        public int Id { get; set; }
    }
}