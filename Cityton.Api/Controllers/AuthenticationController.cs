using System;
using System.Threading.Tasks;
using Cityton.Api.Handlers;
using Cityton.Api.Contracts.Requests.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController: ControllerBase
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
    }
}
