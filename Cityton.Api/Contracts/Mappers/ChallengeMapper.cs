using System.Linq;
using System.Collections.Generic;
using Cityton.Api.Contracts.DTOs;
using Cityton.Api.Data.Models;

namespace Cityton.Api.Contracts.Mappers
{
    public static class ChallengeMapper
    {
        public static ChallengeDTO ToDTO(this Challenge data, int totalUsers)
        {
            if (data == null) return null;

            return new ChallengeDTO
            {
                Id = data.Id,
                Statement = data.Statement,
                Title = data.Title,
                CreatedAt = data.CreatedAt,
                SuccesRate = (data.Achievements.Count() / totalUsers) * 100
            };
        }

        public static List<ChallengeDTO> ToDTO(this List<Challenge> data, int totalUsers)
        {
            return data.Select(c => c.ToDTO(totalUsers)).ToList();
        }

        public static ChallengeMinimalDTO ToChallengeMinimalDTO(this Challenge data)
        {
            if (data == null) return null;

            return new ChallengeMinimalDTO
            {
                Id = data.Id,
                Statement = data.Statement,
                Title = data.Title,
            };
        }

        public static List<ChallengeMinimalDTO> ToChallengeMinimalDTO(this List<Challenge> data)
        {
            return data.Select(c => c.ToChallengeMinimalDTO()).ToList();
        }
    }
}
