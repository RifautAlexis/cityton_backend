using System;
using Cityton.Api.Contracts.DTOs;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;

namespace Cityton.Api.Handlers.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this SignupDTO data)
        {
            if (data == null) return null;

            return new User
            {
                Username = data.Username,
                Email = data.Email,
                Picture = "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png",
                Role = Role.Member,
                PasswordHash = null,
                PasswordSalt = null,
                Token = null,
                CompanyId = 1
            };
        }
    }
}
