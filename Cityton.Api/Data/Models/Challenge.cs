using System;
using System.Collections.Generic;

namespace Cityton.Api.Data.Models
{
    public class Challenge : BaseEntities
    {
        public string Statement { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; } = Status.Waiting;
        public DateTime CreatedAt { get; set; }

        /*****/

        public virtual User Author { get; set; }
        public virtual ICollection<Achievement> Achievements { get; set; }
        public virtual ICollection<ChallengeGiven> ChallengeGivens { get; set; }

        /*****/

        public int? AuthorId { get; set; }

    }
}
