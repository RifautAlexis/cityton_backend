using Cityton.Api.Contracts.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests.User
{
    public class ChangePasswordRequest
    {
        [FromBody]
        public ChangePasswordDTO changePasswordDTO { get; set; }
    }
}
