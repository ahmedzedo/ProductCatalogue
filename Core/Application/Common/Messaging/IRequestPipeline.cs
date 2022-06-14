using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Common.Messaging
{
    public delegate Task<Response<TResponse>> MyRequestHandlerDelegate<TResponse>();
    public interface IRequestPipeline<TRequest, TResponse>
    {
        Task<Response<TResponse>> Handle(TRequest request, CancellationToken cancellationToken, MyRequestHandlerDelegate<TResponse> next);
    }
}
