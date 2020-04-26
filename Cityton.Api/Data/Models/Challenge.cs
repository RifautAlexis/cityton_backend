using System;
using System.Collections.Generic;

namespace Cityton.Api.Data.Models
{
    public class Challenge : BaseEntities
    {
        public string Title { get; set; }
        public string Statement { get; set; }
        public DateTime CreatedAt { get; set; }

        /*****/

        public virtual User Author { get; set; }
        public virtual ICollection<Achievement> Achievements { get; set; }
        public virtual ICollection<ChallengeGiven> ChallengeGivens { get; set; }

        /*****/

        public int? AuthorId { get; set; }

    }
}
