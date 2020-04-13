using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Cityton.Api.Contracts.Requests.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cityton.Api.Handlers.Authentication
{

    public class LoginHandler : IHandler<LoginRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public LoginHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(LoginRequest request)
        {

            (string email, string password) = request.loginDTO;

            User user = await _appDBContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

            Console.WriteLine(user == null);

            if (user == null) { return new NotFoundObjectResult("No user was found for this email"); }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) { return new BadRequestObjectResult("Wrong password"); }

            return new OkObjectResult(user.Token);
        }

        /**************************************************/

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");

            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");

            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }

}
