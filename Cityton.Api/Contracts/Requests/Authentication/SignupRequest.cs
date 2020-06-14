using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Contracts.DTOs;

namespace Cityton.Api.Contracts.Requests
{
    public class SignupRequest
    {
        [FromForm]
        public SignupDTO SignupDTO { get; set; }
    }
}
