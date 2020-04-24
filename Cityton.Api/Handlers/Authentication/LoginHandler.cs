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
using Microsoft.Extensions.Configuration;

namespace Cityton.Api.Handlers.Authentication
{

    public class LoginHandler : IHandler<LoginRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;
        private readonly IConfiguration _appSettings;

        public LoginHandler(ApplicationDBContext appDBContext, IConfiguration config)
        {
            _appDBContext = appDBContext;
            _appSettings = config;
        }

        public async Task<ObjectResult> Handle(LoginRequest request)
        {

            (string email, string password) = request.loginDTO;

            User user = await _appDBContext.Users.Where(u => u.Email == email).Include(u => u.ParticipantGroups).FirstOrDefaultAsync();
            
            string tokenSecret = this._appSettings.GetSection("Settings:Secret").Value;
            user.CreateToken(tokenSecret);

            await _appDBContext.SaveChangesAsync();

            if (user == null) { return new NotFoundObjectResult("No user was found for this email"); }

            if (!user.VerifyPassword(password)) { return new BadRequestObjectResult("Wrong password"); }

            return new OkObjectResult(user.ToDTO());
        }
    }

}
