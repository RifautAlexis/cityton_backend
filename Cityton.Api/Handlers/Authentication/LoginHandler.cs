using System;
using System.Threading.Tasks;
using Cityton.Api.Requests.Authentication;

namespace Cityton.Api.Handlers.Authentication
{

    public class LoginHandler : IHandler<LoginRequest, string>
    {

        public Task<string> Handle(LoginRequest request)
        {
            return Task.FromResult(request.loginDTO.Email + " " + request.loginDTO.Password);
        }
    }
}
