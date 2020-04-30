using Cityton.Api.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests
{
    public class LoginRequest
    {
        [FromBody]
        public LoginDTO loginDTO { get; set; }

    }
}