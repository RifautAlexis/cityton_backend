using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Data;

namespace Cityton.Api.Contracts.Requests
{
    public class SearchUserRequest
    {
        [FromQuery]
        public string SearchText { get; set; }

        [FromQuery]
        public Role? SelectedRole { get; set; }

    }
}