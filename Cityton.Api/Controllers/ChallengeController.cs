using System;
using System.Threading.Tasks;
using Cityton.Api.Handlers;
using Cityton.Api.Contracts.Requests.Challenge;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Data;

namespace Cityton.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChallengeController : ControllerBase
    {
        [HttpGet("search")]
        [Authorized(Role.Admin)]
        public async Task<IActionResult> Search(SearchRequest request, [FromServices] IHandler<SearchRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }
    }
}
