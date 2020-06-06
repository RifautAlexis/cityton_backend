using System.Linq;
using System.Collections.Generic;
using Cityton.Api.Contracts.DTOs;
using Cityton.Api.Data.Models;

namespace Cityton.Api.Contracts.Mappers
{
    public static class ChallengeGivenMapper
    {
        public static ChallengeGivenMinimalDTO ToChallengeGivenMinimalDTO(this ChallengeGiven data)
        {
            if (data == null) return null;

            return new ChallengeGivenMinimalDTO
            {
                Id = data.Id,
                Statement = data.Challenge.Statement,
                Title = data.Challenge.Title,
            };
        }

        public static List<ChallengeGivenMinimalDTO> ToChallengeGivenMinimalDTO(this List<ChallengeGiven> data)
        {
            return data.Select(cg => cg.ToChallengeGivenMinimalDTO()).ToList();
        }
    }
}
