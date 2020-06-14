using Microsoft.AspNetCore.Http;

namespace Cityton.Api.Contracts.DTOs
{
    public class SignupDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
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
