using Microsoft.AspNetCore.Mvc;
using System;

namespace Cityton.Api.Contracts.Requests.Challenge
{
    public class SearchRequest
    {
        [FromQuery]
        public string searchText { get; set; }

        [FromQuery]
        public DateTime? date { get; set; }

    }
}