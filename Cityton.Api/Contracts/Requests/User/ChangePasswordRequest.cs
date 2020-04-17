using Cityton.Api.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests.User
{
    public class ChangePasswordRequest
    {
        [FromBody]
        public ChangePasswordDTO changePasswordDTO { get; set; }
    }
}
