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
        public async Task<IActionResult> Search(SearchChallengeRequest request, [FromServices] IHandler<SearchChallengeRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }

        [HttpPost("add")]
        [Authorized(Role.Admin)]
        public async Task<IActionResult> Add(CreateChallengeRequest request, [FromServices] IHandler<CreateChallengeRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }

        [HttpPost("edit")]
        [Authorized(Role.Admin)]
        public async Task<IActionResult> Edit(UpdateChallengeRequest request, [FromServices] IHandler<UpdateChallengeRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }

        [HttpDelete("delete/{id}")]
        [Authorized(Role.Admin)]
        public async Task<IActionResult> Delete(DeleteChallengeRequest request, [FromServices] IHandler<DeleteChallengeRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }
    }
}
