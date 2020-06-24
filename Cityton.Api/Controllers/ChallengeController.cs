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
    public class ChallengeController : ControllerBase
    {
        [HttpGet("searchAdmin")]
        [Authorized(Role.Admin)]
        public async Task<IActionResult> Search(AdminSearchChallengeRequest request, [FromServices] IHandler<AdminSearchChallengeRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }

        [HttpGet("searchProgression")]
        [Authorized(Role.Checker, Role.Admin)]
        public async Task<IActionResult> Search(ProgressionSearchChallengeRequest request, [FromServices] IHandler<ProgressionSearchChallengeRequest, ObjectResult> handler)
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

        [HttpPut("validate/{id}")]
        [Authorized(Role.Admin, Role.Checker)]
        public async Task<IActionResult> Validate(ValidateChallengeRequest request, [FromServices] IHandler<ValidateChallengeRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }

        [HttpPut("reject/{id}")]
        [Authorized(Role.Admin, Role.Checker)]
        public async Task<IActionResult> Reject(RejectChallengeRequest request, [FromServices] IHandler<RejectChallengeRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }

        [HttpPut("undo/{id}")]
        [Authorized(Role.Admin, Role.Checker)]
        public async Task<IActionResult> Undo(UndoChallengeRequest request, [FromServices] IHandler<UndoChallengeRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }

        [HttpPost("attributeToGroup")]
        [Authorized(Role.Admin)]
        public async Task<IActionResult> AttributeToGroup(AttributeChallengeToGroupRequest request, [FromServices] IHandler<AttributeChallengeToGroupRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }
    }
}
