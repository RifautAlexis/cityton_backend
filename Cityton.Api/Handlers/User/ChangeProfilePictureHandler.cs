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
using Cityton.Api.Handlers.Helpers;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;

namespace Cityton.Api.Handlers
{
    public class ChangeProfilePictureHandler : IHandler<ChangeProfilePictureRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChangeProfilePictureHandler(ApplicationDBContext appDBContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDBContext = appDBContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ObjectResult> Handle(ChangeProfilePictureRequest request)
        {
            System.Console.WriteLine("!!!!! HANDLER !!!!!");
            System.Console.WriteLine(request == null);
            System.Console.WriteLine(request.File == null);
            System.Console.WriteLine(request.File);

            int currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value);

            Account account = new Account(
                "dakczk6el",
                "652331788734115",
                "VPc2ldz94kaKQGohMOhEEd0I5jY");

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

            System.Console.WriteLine("!!!!! END HANDLER !!!!!");

            return new OkObjectResult(true);
        }
    }
}