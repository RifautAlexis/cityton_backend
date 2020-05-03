using System;
using System.Linq;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cityton.Api.Contracts.Requests;
using Cityton.Api.Contracts.DTOs;
using Cityton.Api.Handlers.Mappers;
using System.Collections.Generic;
using System.Security.Claims;

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

        private async Task<Message> NewMessage(string message, int currentUserId, int discussionId, string imageUrl)
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

            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                Media mediaToAdd = new Media
                {
                    Location = imageUrl,
                    CreatedAt = DateTime.Now,
                    MessageId = messageToAdd.Id
                };

                await _appDBContext.Medias.AddAsync(mediaToAdd);
                await _appDBContext.SaveChangesAsync();

                messageToAdd.MediaId = mediaToAdd.Id;
                await _appDBContext.SaveChangesAsync();

                messageToAdd.Media = mediaToAdd;

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