using System;
using System.Threading.Tasks;

namespace Cityton.Api.Handlers
{
    public interface IHandler<TRequest, TResult>
    {
        Task<TResult> Handle(TRequest request);
    }
}
