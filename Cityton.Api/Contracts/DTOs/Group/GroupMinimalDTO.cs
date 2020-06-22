namespace Cityton.Api.Contracts.DTOs
{
    public class GroupMinimalDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasReachMinSize { get; set; }
        public bool HasReachMaxSize { get; set; }
        public UserMinimalDTO Supervisor { get; set; }

    }

}