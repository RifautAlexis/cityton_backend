using System;
namespace Cityton.Api.Data.Models
{
    public class UserInDiscussion : BaseEntities
    {

        public DateTime JoinedAt { get; set; }

        /*****/

        public virtual User Participant { get; set; }
        public virtual Discussion Discussion { get; set; }

        /*****/

        public int ParticipantId { get; set; }
        public int DiscussionId { get; set; }

    }
}
