using System;
using System.Linq;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cityton.Api.Contracts.Requests;
using Cityton.Api.Contracts.DTOs;
using System.Collections.Generic;
using Cityton.Api.Handlers.Mappers;

namespace Cityton.Api.Handlers
{
    public class SearchChallengeHandler : IHandler<SearchChallengeRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public SearchChallengeHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(SearchChallengeRequest request)
        {
            StringComparison comparison = StringComparison.OrdinalIgnoreCase;
            
            List<Challenge> challenges = await _appDBContext.Challenges
                .Where(c => (string.IsNullOrEmpty(request.SearchText) || c.Title.Contains(request.SearchText, comparison) || c.Statement.Contains(request.SearchText, comparison) && (request.Date == null || c.CreatedAt >= request.Date)))
                .OrderByDescending(c => c.CreatedAt).Include(c => c.Achievements)
                .ToListAsync();

            int totalUsers = await _appDBContext.Users.CountAsync();

            List<ChallengeDTO> challengesDTO = challenges.Select(c => c.ToDTO(totalUsers)).ToList();

            return new OkObjectResult(challengesDTO);
        }
    }
}