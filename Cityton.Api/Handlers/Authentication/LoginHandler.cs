using System;
using System.Threading.Tasks;
using Cityton.Api.Requests.Authentication;

namespace Cityton.Api.Handlers.Authentication
{

    public class LoginHandler<TRequest, TResult> : IHandler<TRequest, TResult>
        where TRequest : LoginRequest
    {

        public Task<TResult> Handle(TRequest request)
        {
            return Task.FromResult(request.loginDTO.Email + " " + request.loginDTO.Password);
        }
    }
}
