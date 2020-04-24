using System;
using System.Linq;
using Cityton.Api.Contracts.DTOs;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;

namespace Cityton.Api.Handlers.Mappers
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
                Name = data.Name,
                CreatedAt = data.CreatedAt,
                SuccesRate = (data.Achievements.Count() / totalUsers) * 100
            };
        }
    }
}
