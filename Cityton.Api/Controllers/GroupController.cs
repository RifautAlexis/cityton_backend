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
        
        [HttpGet("searchGroup")]
        [Authorized(Role.Admin, Role.Checker, Role.Member)]
        public async Task<IActionResult> SearchGroup(SearchGroupRequest request, [FromServices] IHandler<SearchGroupRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }

        [HttpPost("createGroup")]
        [Authorized(Role.Member)]
        public async Task<IActionResult> CreateGroup(CreateGroupRequest request, [FromServices] IHandler<CreateGroupRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }
        
        [HttpGet("getGroupInfo/{id}")]
        [Authorized(Role.Admin, Role.Checker, Role.Member)]
        public async Task<IActionResult> GetGroupInfo(GetGroupInfoRequest request, [FromServices] IHandler<GetGroupInfoRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }
        
        [HttpDelete("deleteGroup/{id}")]
        [Authorized(Role.Admin, Role.Member)]
        public async Task<IActionResult> DeleteGroup(DeleteGroupRequest request, [FromServices] IHandler<DeleteGroupRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }
        
        [HttpDelete("deleteMemberShip/{id}")]
        [Authorized(Role.Member)]
        public async Task<IActionResult> DeleteMemberShip(DeleteGroupMembershipRequest request, [FromServices] IHandler<DeleteGroupMembershipRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }
        
        [HttpDelete("deleteGroupRequest/{id}")]
        [Authorized(Role.Member)]
        public async Task<IActionResult> DeleteGroupRequest(DeleteGroupRequestRequest request, [FromServices] IHandler<DeleteGroupRequestRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }
        
        [HttpDelete("acceptGroupRequest/{id}")]
        [Authorized(Role.Member)]
        public async Task<IActionResult> AcceptGroupRequest(AcceptGroupRequestRequest request, [FromServices] IHandler<AcceptGroupRequestRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }
        
        [HttpPut("editName/{id}")]
        [Authorized(Role.Member)]
        public async Task<IActionResult> EditName(EditGroupNameRequest request, [FromServices] IHandler<EditGroupNameRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }
        
        [HttpPost("createRequest")]
        [Authorized(Role.Member)]
        public async Task<IActionResult> CreateRequest(CreateGroupRequestRequest request, [FromServices] IHandler<CreateGroupRequestRequest, ObjectResult> handler)
        {
            return await handler.Handle(request);
        }
    }
}
