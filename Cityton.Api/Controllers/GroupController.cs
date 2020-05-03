using System.Threading.Tasks;
using Cityton.Api.Handlers;
using Cityton.Api.Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Data;

namespace Cityton.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GroupController : ControllerBase
    {
        [HttpGet("getProgression/{id}")]
        [Authorized(Role.Admin, Role.Checker, Role.Member)]
        public async Task<IActionResult> GetProgressionByThreadId(GetProgressionRequest request, [FromServices] IHandler<GetProgressionRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }
    }
}
