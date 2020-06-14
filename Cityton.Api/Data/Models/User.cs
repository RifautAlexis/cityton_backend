using System.Collections.Generic;

namespace Cityton.Api.Data.Models
{
    public class User : BaseEntities
    {

        public string Username { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; } = "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png";
        public Role Role { get; set; } = Role.Member;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Token { get; set; }

        /*****/

        public virtual Company Company { get; set; }
        public virtual ICollection<ParticipantGroup> ParticipantGroups { get; set; }
        public virtual ICollection<Challenge> Challenges { get; set; }
        public virtual ICollection<Achievement> Achievements { get; set; }
        public virtual ICollection<UserInDiscussion> UsersInDiscussion { get; set; }
        public virtual ICollection<Message> MessagesWriten { get; set; }
        public virtual ICollection<Group> GroupsSupervised { get; set; }

        /*****/

        public int CompanyId { get; set; }

    }
}
