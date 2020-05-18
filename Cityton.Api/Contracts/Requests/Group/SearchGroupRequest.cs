using Cityton.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace Cityton.Api.Contracts.Requests
{
    public class SearchGroupRequest
    {
        [FromQuery]
        public string GroupName { get; set; }
        [FromQuery]
        public FilterGroup SelectedFilter { get; set; }


        internal void Deconstruct(out string groupName, out FilterGroup selectedFilter)
        {
            groupName = GroupName;
            selectedFilter = SelectedFilter;
        }

    }
}