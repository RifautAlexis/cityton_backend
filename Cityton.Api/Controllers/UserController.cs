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
    public class UserController : ControllerBase
    {

        [HttpPost]
        [Authorized(Role.Member, Role.Checker, Role.Admin)]
        [Route("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request, [FromServices] IHandler<ChangePasswordRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }

        [HttpGet]
        [Authorized(Role.Member, Role.Checker, Role.Admin)]
        [Route("getProfile/{id}")]
        public async Task<IActionResult> GetProfile(GetProfileRequest request, [FromServices] IHandler<GetProfileRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }

        [HttpGet]
        [Authorized(Role.Member, Role.Checker, Role.Admin)]
        [Route("search")]
        public async Task<IActionResult> GetProfile(SearchUserRequest request, [FromServices] IHandler<SearchUserRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }

        [HttpDelete("delete/{id}")]
        [Authorized(Role.Admin)]
        public async Task<IActionResult> Delete(DeleteUserRequest request, [FromServices] IHandler<DeleteUserRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }

        [HttpPut]
        [Authorized(Role.Member, Role.Checker, Role.Admin)]
        [Route("changeProfilePicture")]
        public async Task<IActionResult> ChangeProfilePicture(ChangeProfilePictureRequest request, [FromServices] IHandler<ChangeProfilePictureRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }

        [HttpGet]
        [Authorized(Role.Admin)]
        [Route("getAllStaffMember")]
        public async Task<IActionResult> GetAllStaffMember(GetAllStaffMemberRequest request, [FromServices] IHandler<GetAllStaffMemberRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }

        [HttpPut]
        [Authorized(Role.Admin)]
        [Route("changeRole/{id}")]
        public async Task<IActionResult> ChangeRole(ChangeRoleRequest request, [FromServices] IHandler<ChangeRoleRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }
    }
}
