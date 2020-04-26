using System;

namespace Cityton.Api.Contracts.DTOs.Challenge
{
    public class ChallengeDTO
    {
        public int Id { get; set; }
        public string Statement { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public double SuccesRate { get; set; }
    }
}
