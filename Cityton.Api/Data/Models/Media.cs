using System;
namespace Cityton.Api.Data.Models
{
    public class Media : BaseEntities
    {
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }

        /*****/

        public virtual Message ContainedIn { get; set; }

        /*****/

        public int MessageId { get; set; }


    }
}
