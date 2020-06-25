using System;
namespace Cityton.Api.Data.Models
{
    public class ParticipantGroup : BaseEntities
    {

        public bool IsCreator { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }

        /*****/

        public virtual Group BelongingGroup { get; set; }
        public virtual User User { get; set; }

        /*****/

        public int BelongingGroupId { get; set; }
        public int UserId { get; set; }

    }
}
