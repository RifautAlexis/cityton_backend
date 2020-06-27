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
using Cityton.Api.Contracts.Mappers;

namespace Cityton.Api.Handlers
{
    public class SearchGroupHandler : IHandler<SearchGroupRequest, ObjectResult>
    {
        private readonly ApplicationDBContext _appDBContext;

        public SearchGroupHandler(ApplicationDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ObjectResult> Handle(SearchGroupRequest request)
        {
            (string groupName, FilterGroup selectedFilter) = request;

            StringComparison comparison = StringComparison.OrdinalIgnoreCase;

            List<Group> groups = await _appDBContext.Groups
                .Where(g => string.IsNullOrEmpty(groupName) || g.Name.Contains(groupName, comparison))
                .OrderBy(c => c.Name)
                .Include(g => g.Supervisor)
                .Include(g => g.Members)
                .ThenInclude(pg => pg.User)
                .ToListAsync();


            (int groupMinSize, int groupMaxSize) = await _appDBContext.Companies.Where(c => c.Id == 1).FirstOrDefaultAsync();
            System.Console.WriteLine("MaxSize => " + groupMaxSize);
            System.Console.WriteLine("MinSize => " + groupMinSize);

            if (selectedFilter != FilterGroup.All)
            {
                if (selectedFilter == FilterGroup.Full)
                {
                    groups = groups.Where(g => g.Members.Where(pg => pg.Status == Status.Accepted).Count() == groupMaxSize).ToList();
                }
                else if (selectedFilter == FilterGroup.NotFull)
                {
                    groups = groups.Where(g => g.Members.Where(pg => pg.Status == Status.Accepted).Count() < groupMaxSize).ToList();
                }
                else
                {
                    groups = groups.Where(g => g.Members.Where(pg => pg.Status == Status.Accepted).Count() < groupMinSize).ToList();
                }
            }

            System.Console.WriteLine("====================");
            foreach (var group in groups)
            {
                System.Console.WriteLine(group.Name + " => " + group.Members.Count);
                System.Console.WriteLine(group.Members.Where(pg => pg.Status == Status.Accepted).Count());
            }
            System.Console.WriteLine("====================");

            List<GroupMinimalDTO> groupsDTO = groups.ToGroupMinimalDTO(groupMinSize, groupMaxSize);

            return new OkObjectResult(groupsDTO);
        }
    }
}