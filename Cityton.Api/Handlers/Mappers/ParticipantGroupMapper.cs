using System.Linq;
using Cityton.Api.Contracts.DTOs;
using Cityton.Api.Data.Models;
using System.Collections.Generic;

namespace Cityton.Api.Handlers.Mappers
{
    public static class ParticipantGroupMapper
    {
        public static ParticipantGroupMinimalDTO ToParticipantGroupMinimalDTO(this ParticipantGroup data)
        {
            if (data == null) return null;
            
            return new ParticipantGroupMinimalDTO
            {
                Id = data.Id,
                User = data.User.ToUserMinimalDTO(),
            };
        }

        public static List<ParticipantGroupMinimalDTO> ToParticipantGroupMinimalDTO(this List<ParticipantGroup> data)
        {
            return data.Select(d => d.ToParticipantGroupMinimalDTO()).ToList();
        }
    }
}
