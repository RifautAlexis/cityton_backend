using System;
namespace Cityton.Api.Data.Models
{
    public class Message : BaseEntities
    {

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        /*****/

        public virtual User Author { get; set; }
        public virtual Discussion Discussion { get; set; }
        public virtual Media Media { get; set; }

        /*****/

        public int? AuthorId { get; set; }
        public int DiscussionId { get; set; }
        public int? MediaId { get; set; }

    }
}
