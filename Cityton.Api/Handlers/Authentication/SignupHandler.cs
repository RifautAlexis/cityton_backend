using System;
using System.Linq;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Cityton.Api.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cityton.Api.Contracts.Mappers;
using Microsoft.Extensions.Configuration;
using Cityton.Api.Handlers.Helpers;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Cityton.Api.Handlers
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
            (string username, string email, string password, IFormFile profilePicture) = request;

            Account account = new Account(
                this._appSettings.GetSection("Cloudinary:CloudName").Value,
                this._appSettings.GetSection("Cloudinary:ApiKey").Value,
                this._appSettings.GetSection("Cloudinary:ApiSecret").Value);

            Cloudinary cloudinary = new Cloudinary(account);

            Stream stream = profilePicture.OpenReadStream();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(username, stream),
                PublicId = this._appSettings.GetSection("Cloudinary:ProfilePicturesFolder").Value + username
            };

            var uploadResult = cloudinary.Upload(uploadParams);

            User user = new User{
                Username = username,
                Email = email,
                Picture = this._appSettings.GetSection("Cloudinary:BaseUrl").Value + uploadResult.SecureUri.AbsolutePath,
                Role = Role.Member,
                CompanyId = 1
            };

            user.ParticipantGroups = null;

            user.CreatePassword(password);

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
                DiscussionId = discussioGeneralId,
                ParticipantId = user.Id
            };

            await _appDBContext.UsersInDiscussion.AddAsync(userInGeneral);

            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(user.ToDTO());
        }
    }
}
