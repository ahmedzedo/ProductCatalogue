using ProductCatalogue.Application.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Common.Behaviours
{
    public class RequestPipeline<TRequest, TResponse> : IRequestPipeline<TRequest, TResponse>
        where TRequest : IBaseRequest<TResponse>
    {
        public async Task<Response<TResponse>> Handle(TRequest request, CancellationToken cancellationToken,MyRequestHandlerDelegate<TResponse> next)
        {
            try
            {
                Debug.WriteLine("before request");
                var response = await next();
                Debug.WriteLine("after request");

                return  response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
