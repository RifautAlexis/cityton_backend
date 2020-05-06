using System.Collections.Generic;

namespace Cityton.Api.Contracts.DTOs
{
    public class GroupProgressionDTO
    {

        public int GroupId { get; set; }
        public double Progression { get; set; }
        public List<ChallengeGivenMinimalDTO> InProgress { get; set; }
        public List<ChallengeGivenMinimalDTO> Succeed { get; set; }
        public List<ChallengeGivenMinimalDTO> Failed { get; set; }

    }

}