using Cityton.Api.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests
{
    public class ChangePasswordRequest
    {
        [FromBody]
        public ChangePasswordDTO ChangePasswordDTO { get; set; }
    }
}
