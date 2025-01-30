using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timezones.API.Shared.Handlers
{
    public interface IHandler<TSchema, TResponse>
    {
        Task<HandlerResponse<TResponse>> Execute(TSchema request);
    }
}