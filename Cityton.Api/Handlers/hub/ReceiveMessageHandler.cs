using System;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Contracts.DTOs;
using Cityton.Api.Contracts.Mappers;

namespace Cityton.Api.Handlers
{
    public class ReceiveMessageHandler : IHandlerHub<CreateMessageDTO, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public ReceiveMessageHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(CreateMessageDTO request, int connectedUserId)
        {
            (string message, int discussionId, string imageUrl) = request;

            Message messageAdded = await NewMessage(message, connectedUserId, discussionId, imageUrl);

            return new OkObjectResult(messageAdded.ToDTO());
        }

        private async Task<Message> NewMessage(string message, int currentUserId, int discussionId, string mediaUrl)
        {
            Message messageToAdd = new Message
            {
                Content = message,
                CreatedAt = DateTime.Now,
                AuthorId = currentUserId,
                DiscussionId = discussionId,
                MediaId = null
            };

            await _appDBContext.Messages.AddAsync(messageToAdd);
            await _appDBContext.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(mediaUrl))
            {
                Media mediaToAdd = new Media
                {
                    Location = mediaUrl,
                    CreatedAt = DateTime.Now,
                    MessageId = messageToAdd.Id
                };

                await _appDBContext.Medias.AddAsync(mediaToAdd);
                await _appDBContext.SaveChangesAsync();

                messageToAdd.MediaId = mediaToAdd.Id;
                await _appDBContext.SaveChangesAsync();

            }
            else
            {
                messageToAdd.Media = null;
            }

            messageToAdd.Author = await _appDBContext.Users.FindAsync(currentUserId);

            return messageToAdd;
        }
    }
}