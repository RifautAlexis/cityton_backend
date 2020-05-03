using System;

namespace Cityton.Api.Contracts.DTOs
{
    public class MessageDTO
    {

        public int Id { get; set; }
        public string Content { get; set; }
        public MediaDTO Media { get; set; }
        public UserMinimalDTO Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public int DiscussionId { get; set; }

    }

}