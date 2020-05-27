using Cityton.Api.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Cityton.Api.Contracts.Requests
{
    public class SignupRequest
    {
        [FromForm]
        public string Username { get; set; }
        [FromForm]
        public string Email { get; set; }
        [FromForm]
        public string Password { get; set; }
        [FromForm]
        public IFormFile ProfilePicture { get; set; }

        internal void Deconstruct(out string username, out string email, out string password, out IFormFile profilePicture)
        {
            username = Username;
            email = Email;
            password = Password;
            profilePicture = ProfilePicture;
        }
    }
}
