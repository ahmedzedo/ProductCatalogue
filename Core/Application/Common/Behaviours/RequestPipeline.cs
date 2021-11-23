using ProductCatalogue.Application.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Common.Behaviours
{
    public class RequestPipeline<TRequest, TResponse> : IRequestPipeline<TRequest, TResponse>
    {
        public async Task<Response<TResponse>> Handle(TRequest request, Func<TRequest, CancellationToken, Task<Response<TResponse>>> next, CancellationToken cancellationToken)
        {
            try
            {
                var response = await next(request,cancellationToken);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
