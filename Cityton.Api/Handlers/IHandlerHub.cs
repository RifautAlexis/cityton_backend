using System;
using System.Threading.Tasks;

namespace Cityton.Api.Handlers
{
    public interface IHandlerHub<TRequest, TResult>
    {
        Task<TResult> Handle(TRequest request, int id);
    }
}
