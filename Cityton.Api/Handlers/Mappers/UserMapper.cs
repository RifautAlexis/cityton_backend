using System.Linq;
using Cityton.Api.Contracts.DTOs.User;
using Cityton.Api.Contracts.DTOs.Authentication;
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
                Picture = data.Picture == null ? "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png" : data.Picture,
                Role = Role.Member,
                PasswordHash = null,
                PasswordSalt = null,
                Token = null,
                CompanyId = 1
            };
        }

        public static UserDTO ToDTO(this User data)
        {
            if (data == null) return null;

            return new UserDTO
            {
                Id = data.Id,
                Username = data.Username,
                Email = data.Email,
                Picture = data.Picture,
                Role = data.Role,
                Token = data.Token,
                GroupId = data.ParticipantGroups?.Where(pg => pg.Status == Status.Accepted).Select(pg => pg.BelongingGroupId).FirstOrDefault(),
            };
        }

        public static UserProfileDTO ToUserProfile(this User data)
        {
            if (data == null) return null;

            return new UserProfileDTO
            {
                Id = data.Id,
                Username = data.Username,
                Email = data.Email,
                Picture = data.Picture,
                Role = data.Role,
                GroupName = data.ParticipantGroups?.Where(pg => pg.Status == Status.Accepted).Select(pg => pg.BelongingGroup.Name).FirstOrDefault(),
            };
        }
    }
}
