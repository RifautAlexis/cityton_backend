using Cityton.Api.Data;

namespace Cityton.Api.Contracts.DTOs
{
    public class ParticipantGroupMinimalDTO
    {
        public int Id { get; set; }
        public UserMinimalDTO User { get; set; }
    }
}
