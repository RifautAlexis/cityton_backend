using Microsoft.AspNetCore.Mvc;
using System;

namespace Cityton.Api.Contracts.Requests
{
    public class AdminSearchChallengeRequest
    {
        [FromQuery]
        public string SearchText { get; set; }

        [FromQuery]
        public DateTime? Date { get; set; }

        internal void Deconstruct(out string searchText, out DateTime? date)
        {
            searchText = SearchText;
            date = Date;
        }

    }
}