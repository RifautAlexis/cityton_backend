using System;
using System.Linq;
using System.Threading.Tasks;
using Cityton.Api.Data;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cityton.Api.Contracts.Requests.Challenge;
using Cityton.Api.Contracts.DTOs.Challenge;
using System.Collections.Generic;
using Cityton.Api.Handlers.Mappers;

namespace Cityton.Api.Handlers.Authentication
{
    public class SearchHandler : IHandler<SearchRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public SearchHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(SearchRequest request)
        {
            StringComparison comparison = StringComparison.OrdinalIgnoreCase;
            
            List<Challenge> challenges = await _appDBContext.Challenges
                .Where(c => (string.IsNullOrEmpty(request.searchText) || c.Title.Contains(request.searchText, comparison) || c.Statement.Contains(request.searchText, comparison) && (request.date == null || c.CreatedAt >= request.date)))
                .OrderByDescending(c => c.CreatedAt).Include(c => c.Achievements)
                .ToListAsync();

            int totalUsers = await _appDBContext.Users.CountAsync();

            List<ChallengeDTO> challengesDTO = challenges.Select(c => c.ToDTO(totalUsers)).ToList();

            return new OkObjectResult(challengesDTO);
        }
    }
}