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
    public class RemoveMessageHandler : IHandler<int, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public RemoveMessageHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(int id)
        {

            Message messageToRemove = await _appDBContext.Messages
                .Where(m => m.Id == id)
                .Include(m => m.Author)
                .Include(m => m.Media)
                .FirstOrDefaultAsync();

            messageToRemove.Content = null;
            await _appDBContext.SaveChangesAsync();

            return new OkObjectResult(messageToRemove.ToDTO());
        }
    }
}