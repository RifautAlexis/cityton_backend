using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Cityton.Api.Contracts.Requests.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cityton.Api.Handlers.Mappers;
using Cityton.Api.Handlers.Helpers;

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

            User user = await _appDBContext.Users.Where(u => u.Email == email).Include(u => u.ParticipantGroups).FirstOrDefaultAsync();

            if (user == null) { return new NotFoundObjectResult("No user was found for this email"); }

            if (!user.VerifyPassword(password)) { return new BadRequestObjectResult("Wrong password"); }

            return new OkObjectResult(user.ToDTO());
        }
    }

}
