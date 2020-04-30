using Microsoft.AspNetCore.Mvc;
using System;

namespace Cityton.Api.Contracts.Requests
{
    public class SearchChallengeRequest
    {
        [FromQuery]
        public string SearchText { get; set; }

        [FromQuery]
        public DateTime? Date { get; set; }

    }
}