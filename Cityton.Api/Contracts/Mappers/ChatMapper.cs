using System.Collections.Generic;
using System.Linq;
using Cityton.Api.Contracts.DTOs;
using Cityton.Api.Data.Models;

namespace Cityton.Api.Contracts.Mappers
{
    public static class ChatMapper
    {

        public static ThreadDTO ToThreadDTO(this Discussion data)
        {
            if (data == null) return null;

            return new ThreadDTO
            {
                DiscussionId = data.Id,
                Name = data.Name
            };
        }

        public static List<ThreadDTO> ToThreadsDTO(this List<Discussion> data)
        {
            return data.Select(d => d.ToThreadDTO()).ToList();
        }
    }
}
