using ProductCatalogue.Application.Common.Messaging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Common.Behaviours
{
    public class LoggerPipline<TRequest, TResponse> : IRequestResponsePipeline<TRequest, TResponse>
        where TRequest : IBaseRequest<TResponse>
    {
        #region Dependencies

        #endregion

        #region Constructor
        public LoggerPipline()
        {

        }
        #endregion

        #region Handel
        public async Task<IResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken, MyRequestResponseHandlerDelegate<TResponse> next)
        {
            try
            {
                Debug.WriteLine($"Logger Pipline Before Rquest ");
                var response = await next();
                Debug.WriteLine($"Logger Pipline after Rquest ");

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
