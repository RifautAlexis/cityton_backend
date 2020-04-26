using Cityton.Api.Contracts.DTOs.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests.Authentication
{
    public class LoginRequest
    {
        [FromBody]
        public LoginDTO loginDTO { get; set; }

    }
}