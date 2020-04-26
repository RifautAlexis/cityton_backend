using System;
using System.Threading.Tasks;
using Cityton.Api.Handlers;
using Cityton.Api.Contracts.Requests.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Data;
using Cityton.Api.Contracts.Validators;
using FluentValidation.Results;
using FluentValidation.AspNetCore;

namespace Cityton.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDBContext _appDBContext;

        public AuthenticationController(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

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
            // SignupDTOValidator validator = new SignupDTOValidator(_appDBContext);
            // ValidationResult results = await validator.ValidateAsync(request.signupDTO);
            
            // results.AddToModelState(ModelState, "SignupDTO");
            
            if (!ModelState.IsValid) return BadRequest(this.ModelState);
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
