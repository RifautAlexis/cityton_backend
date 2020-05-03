using Cityton.Api.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests
{
    public class ReceiveMessageRequest
    {
        public CreateMessageDTO CreateMessageDTO { get; set; }
    }
}
