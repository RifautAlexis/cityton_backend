using System.Collections.Generic;

namespace Cityton.Api.Contracts.DTOs
{
    public class GroupMinimalDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasReachMaxSize { get; set; }

    }

}