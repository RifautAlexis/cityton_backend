using System;
using System.Threading.Tasks;
using Cityton.Api.Handlers;
using Cityton.Api.Requests.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController: ControllerBase
    {

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("login")]
        //public async Task<IActionResult> Login(LoginRequest request, [FromServices] IHandler<LoginRequest, string> handler)
        //{

        //    var result = await handler.Handle(request);
        //    return Ok(result);
        //}

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request, [FromServices] IHandler<LoginRequest, string> handler)
        {

            var result = await handler.Handle(request);
            return Ok(result);
        }
    }
}
