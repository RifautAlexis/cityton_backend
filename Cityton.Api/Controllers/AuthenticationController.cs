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
    public class AuthenticationController : ControllerBase
    {

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request, [FromServices] IHandler<LoginRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("signup")]
        public async Task<IActionResult> Signup(SignupRequest request, [FromServices] IHandler<SignupRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }

        [Authorized(Role.Member, Role.Checker, Role.Admin)]
        [HttpGet("")]
        public async Task<IActionResult> GetConnectedUser(GetConnectedUserRequest request, [FromServices] IHandler<GetConnectedUserRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }
    }
}
