using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cityton.Api.Contracts.Requests.User;
using Cityton.Api.Handlers.Helpers;

namespace Cityton.Api.Handlers.Authentication
{
    public class ChangePasswordHandler : IHandler<ChangePasswordRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChangePasswordHandler(ApplicationDBContext appDBContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDBContext = appDBContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ObjectResult> Handle(ChangePasswordRequest request)
        {
            (string oldPassword, string newPassword) = request.changePasswordDTO;

            int currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value);
            User user = await _appDBContext.Users.Where(u => u.Id == currentUserId).FirstOrDefaultAsync();

            if (!user.VerifyPassword(oldPassword)) { return new BadRequestObjectResult("Wrong password"); }
            user.CreatePassword(newPassword);

            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(true);
        }
    }
}