using System;
using System.Collections.Generic;

namespace Cityton.Api.Data.Models
{
    public class Discussion : BaseEntities
    {

        public DateTime CreatedAt { get; set; }
        public string Name { get; set; } // peut être null => ""

        /*****/

        public ICollection<UserInDiscussion> UsersInDiscussion { get; set; }
        public ICollection<Message> Messages { get; set; }
        public virtual Group Group { get; set; }

        /*****/

        public int? GroupId { get; set; }

    }
}
