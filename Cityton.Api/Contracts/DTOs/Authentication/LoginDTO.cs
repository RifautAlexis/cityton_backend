using System;
namespace Cityton.Api.Contracts.DTOs.Authentication
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

        internal void Deconstruct(out string email, out string password)
        {
            email = Email;
            password = Password;
        }
    }
}
