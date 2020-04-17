using System;
using System.Threading.Tasks;
using Cityton.Api.Handlers;
using Cityton.Api.Contracts.Requests.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Data;

namespace Cityton.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {

        [HttpPost]
        [Authorized(Role.Member, Role.Checker, Role.Admin)]
        [Route("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request, [FromServices] IHandler<ChangePasswordRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }
    }
}
