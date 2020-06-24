using Microsoft.AspNetCore.Mvc;
using System;

namespace Cityton.Api.Contracts.Requests
{
    public class ProgressionSearchChallengeRequest
    {
        [FromQuery]
        public string SearchText { get; set; }

        [FromQuery]
        public int ThreadId { get; set; }

        internal void Deconstruct(out string searchText, out int threadId)
        {
            searchText = SearchText;
            threadId = ThreadId;
        }

    }
}