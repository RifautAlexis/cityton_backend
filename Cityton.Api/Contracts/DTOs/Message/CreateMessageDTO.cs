using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.DTOs
{
    public class CreateMessageDTO
    {
        [FromQuery]
        public string Message { get; set; }
        [FromQuery]
        public int DiscussionId { get; set; }
        [FromQuery]
        public string ImageUrl { get; set; }

        internal void Deconstruct(out string message, out int discussionId, out string imageUrl)
        {
            message = Message;
            discussionId = DiscussionId;
            imageUrl = ImageUrl;
        }

    }

}