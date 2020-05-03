using System.Collections.Generic;

namespace Cityton.Api.Contracts.DTOs
{
    public class GroupProgressionDTO
    {

        public int GroupId { get; set; }
        public double Progression { get; set; }
        public List<ChallengeMinimalDTO> InProgress { get; set; }
        public List<ChallengeMinimalDTO> Succeed { get; set; }
        public List<ChallengeMinimalDTO> Failed { get; set; }

    }

}