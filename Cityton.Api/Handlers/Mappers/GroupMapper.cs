using System.Collections.Generic;
using System.Linq;
using Cityton.Api.Contracts.DTOs;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;

namespace Cityton.Api.Handlers.Mappers
{
    public static class GroupMapper
    {
        public static GroupDTO ToDTO(this Group data, int maxGroupSize)
        {
            if (data == null) return null;

            return new GroupDTO
            {
                Id = data.Id,
                Name = data.Name,
                Creator = data.Members.Where(pg => pg.IsCreator == true).Select(pg => pg.User.ToUserMinimalDTO()).FirstOrDefault(),
                Members = data.Members.Where(pg => pg.IsCreator == false && pg.Status == Status.Accepted).ToList().ToParticipantGroupMinimalDTO(),
                RequestsAdhesion = data.Members.Where(pg => pg.IsCreator == false && pg.Status == Status.Waiting).ToList().ToParticipantGroupMinimalDTO(),
                HasReachMaxSize = data.Members.Count == maxGroupSize,
            };
        }

        public static List<GroupDTO> ToDTO(this List<Group> data, int maxGroupSize)
        {
            return data.Select(d => d.ToDTO(maxGroupSize)).ToList();
        }

        public static GroupMinimalDTO ToGroupMinimalDTO(this Group data, int maxGroupSize)
        {
            if (data == null) return null;

            return new GroupMinimalDTO
            {
                Id = data.Id,
                Name = data.Name,
                HasReachMaxSize = data.Members.Count == maxGroupSize,
            };
        }

        public static List<GroupMinimalDTO> ToGroupMinimalDTO(this List<Group> data, int maxGroupSize)
        {
            return data.Select(d => d.ToGroupMinimalDTO(maxGroupSize)).ToList();
        }
    }
}
