using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cityton.Api.Contracts.Requests;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Cityton.Api.Handlers
{
    public class ChangeProfilePictureHandler : IHandler<ChangeProfilePictureRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _appSettings;


        public ChangeProfilePictureHandler(ApplicationDBContext appDBContext, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _appDBContext = appDBContext;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = config;
        }

        public async Task<ObjectResult> Handle(ChangeProfilePictureRequest request)
        {
            int currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value);

            Account account = new Account(
                this._appSettings.GetSection("Cloudinary:CloudName").Value,
                this._appSettings.GetSection("Cloudinary:ApiKey").Value,
                this._appSettings.GetSection("Cloudinary:ApiSecret").Value);

            Cloudinary cloudinary = new Cloudinary(account);

            Stream stream = request.File.OpenReadStream();

            User user = await _appDBContext.Users.Where(u => u.Id == currentUserId).FirstOrDefaultAsync();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(user.Username, stream)
            };

            var uploadResult = cloudinary.Upload(uploadParams);

            user.Picture = uploadResult.SecureUri.AbsoluteUri;

            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(true);
        }
    }
}