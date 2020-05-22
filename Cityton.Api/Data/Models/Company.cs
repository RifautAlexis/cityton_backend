using System;
using System.Collections.Generic;

namespace Cityton.Api.Data.Models
{
    public class Company : BaseEntities
    {

        public string Name { get; set; }
        public int MinGroupSize { get; set; }
        public int MaxGroupSize { get; set; }
        public DateTime CreatedAt { get; set; }

        /*****/

        public virtual ICollection<User> Users { get; set; }

        /*****/

        internal void Deconstruct(out int minGroupSize, out int maxGroupSize)
        {
            minGroupSize = MinGroupSize;
            maxGroupSize = MaxGroupSize;
        }
    }
}
