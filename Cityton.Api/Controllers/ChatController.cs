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
    public class ChatController : ControllerBase
    {
        [HttpGet("getThreadsByUserId/{id}")]
        [Authorized(Role.Admin, Role.Checker, Role.Member)]
        public async Task<IActionResult> GetThreadsByUserId(GetThreadsByUserIdRequest request, [FromServices] IHandler<GetThreadsByUserIdRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }

        [HttpGet("getMessages/{id}")]
        [Authorized(Role.Admin, Role.Checker, Role.Member)]
        public async Task<IActionResult> GetMessages(GetMessagesByThreadIdRequest request, [FromServices] IHandler<GetMessagesByThreadIdRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }
    }
}
