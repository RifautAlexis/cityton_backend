using Cityton.Api.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests.Authentication
{
    public class SignupRequest
    {
        [FromBody]
        public SignupDTO signupDTO { get; set; }
    }
}
