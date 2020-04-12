using System;
namespace Cityton.Api.Data.Models
{
    public class ChallengeGiven : BaseEntities
    {

        public StatusChallenge Status { get; set; } = StatusChallenge.InProgress;

        /*****/

        public virtual Challenge Challenge { get; set; }
        public virtual Group ChallengedGroup { get; set; }

        /*****/

        public int ChallengeId { get; set; }
        public int ChallengedGroupId { get; set; }
    }
}
