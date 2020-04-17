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
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Cityton.Api.Handlers.Helpers;
using Cityton.Api.Handlers.Mappers;

namespace Cityton.Api.Handlers.Authentication
{
    public class SignupHandler : IHandler<SignupRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;
        private readonly IConfiguration _appSettings;

        public SignupHandler(ApplicationDBContext appDBContext, IConfiguration config)
        {
            _appDBContext = appDBContext;
            _appSettings = config;
        }

        public async Task<ObjectResult> Handle(SignupRequest request)
        {

            User user = request.signupDTO.ToUser();
            user.ParticipantGroups = null;

            user.CreatePassword(request.signupDTO.Password);

            string tokenSecret = this._appSettings.GetSection("Settings:Secret").Value;
            user.CreateToken(tokenSecret);

            await _appDBContext.Users.AddAsync(user);

            await _appDBContext.SaveChangesAsync();

            int discussioGeneralId = await _appDBContext.Discussions
                .Where(d => d.Name == "general")
                .Select(d => d.Id)
                .FirstOrDefaultAsync();

            UserInDiscussion userInGeneral = new UserInDiscussion
            {
                JoinedAt = DateTime.Now,
                DiscussionId = discussioGeneralId
            };

            await _appDBContext.UsersInDiscussion.AddAsync(userInGeneral);

            return new OkObjectResult(user.ToDTO());
        }
    }
}
