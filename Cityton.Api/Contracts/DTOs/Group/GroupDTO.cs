using System.Collections.Generic;

namespace Cityton.Api.Contracts.DTOs
{
    public class GroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserMinimalDTO Creator { get; set; }
        public List<ParticipantGroupMinimalDTO> Members { get; set; }
        public List<ParticipantGroupMinimalDTO> RequestsAdhesion { get; set; }
        public bool HasReachMaxSize { get; set; }

    }

}