using System;

namespace Cityton.Api.Contracts.DTOs
{
    public class ChallengeDTO
    {
        public int Id { get; set; }
        public string Statement { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public double SuccesRate { get; set; }
    }
}
