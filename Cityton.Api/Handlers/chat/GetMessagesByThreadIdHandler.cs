using System.Linq;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cityton.Api.Contracts.Requests;
using Cityton.Api.Handlers.Mappers;
using System.Collections.Generic;

namespace Cityton.Api.Handlers
{
    public class GetMessagesByThreadIdHandler : IHandler<GetMessagesByThreadIdRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public GetMessagesByThreadIdHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(GetMessagesByThreadIdRequest request)
        {
            Discussion discussion = await _appDBContext.Discussions
                .Where(d => d.Id == request.Id)
                .FirstOrDefaultAsync();

            if (discussion == null) { return new NotFoundObjectResult("No discussion found"); }

            List<Message> Messages = await _appDBContext.Messages
            .Where(m => m.DiscussionId == request.Id)
            .Include(m => m.Media)
            .Include(m => m.Author)
            .ToListAsync();

            return new OkObjectResult(Messages.ToDTO());
        }
    }
}