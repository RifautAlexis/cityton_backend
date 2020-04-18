using Cityton.Api.Data;

namespace Cityton.Api.Contracts.DTOs
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public Role Role { get; set; }
        public string GroupName { get; set; }
    }
}
