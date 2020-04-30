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

namespace Cityton.Api.Handlers
{
    public class GetThreadsByUserIdHandler : IHandler<GetThreadsByUserIdRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public GetThreadsByUserIdHandler(ApplicationDBContext appDBContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(GetThreadsByUserIdRequest request)
        {
            User user = await _appDBContext.Users.Where(u => u.Id == request.Id).FirstOrDefaultAsync();

            if (user == null) { return new NotFoundObjectResult("No user found"); }

            List<Discussion> threads = await _appDBContext.UsersInDiscussion.Where(uid => uid.ParticipantId == request.Id).Select(uid => uid.Discussion).ToListAsync();

            return new OkObjectResult(threads.ToThreadsDTO());
        }
    }
}