using System.Linq;
using Cityton.Api.Contracts.DTOs;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using System.Collections.Generic;

namespace Cityton.Api.Contracts.Mappers
{
    public static class UserMapper
    {

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
                GroupIdsRequested = data.ParticipantGroups?.Where(pg => pg.Status == Status.Waiting).Select(pg => pg.BelongingGroupId).ToList()
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
                GroupName = data.ParticipantGroups?.Where(pg => pg.Status == Status.Accepted).Select(pg => pg.BelongingGroup.Name).FirstOrDefault()
            };
        }

        public static UserMinimalDTO ToUserMinimalDTO(this User data)
        {
            if (data == null) return null;

            return new UserMinimalDTO
            {
                Id = data.Id,
                Username = data.Username,
                ProfilePicture = data.Picture
            };
        }

        public static List<UserMinimalDTO> ToUserMinimalDTO(this List<User> data)
        {
            return data.Select(d => d.ToUserMinimalDTO()).ToList();
        }
    }
}
