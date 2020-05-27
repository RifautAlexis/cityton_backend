using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Cityton.Api.Contracts.Requests
{
    public class ChangeProfilePictureRequest
    {
        [FromForm(Name="file")]
        public IFormFile File { get; set; }
    }
}
