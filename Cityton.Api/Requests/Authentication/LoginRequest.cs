using System;
using Cityton.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Requests.Authentication
{
    public class LoginRequest
    {
        [FromBody]
        public LoginDTO loginDTO { get; set; }

    }
}