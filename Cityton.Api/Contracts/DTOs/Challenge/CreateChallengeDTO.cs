using System;
namespace Cityton.Api.Contracts.DTOs
{
    public class CreateChallengeDTO
    {
        public string Title { get; set; }
        public string Statement { get; set; }

        internal void Deconstruct(out string title, out string statement)
        {
            title = Title;
            statement = Statement;
        }
    }
}
