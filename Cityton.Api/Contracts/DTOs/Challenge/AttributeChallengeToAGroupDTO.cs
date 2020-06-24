using System.Collections.Generic;

namespace Cityton.Api.Contracts.DTOs
{
    public class AttributeChallengeToAGroupDTO
    {
        public int ThreadId { get; set; }
        public List<int> ChallengeIds { get; set; }

        internal void Deconstruct(out int threadId, out List<int> challengeIds)
        {
            threadId = ThreadId;
            challengeIds = ChallengeIds;
        }
    }
}