using System;
namespace Cityton.Api.Contracts.DTOs.Challenge
{
    public class UpdateDTO
    {
        public int ChallengeId { get; set; }
        public string Title { get; set; }
        public string Statement { get; set; }

        internal void Deconstruct(out int challengeId, out string title, out string statement)
        {
            challengeId = ChallengeId;
            title = Title;
            statement = Statement;
        }
    }
}
