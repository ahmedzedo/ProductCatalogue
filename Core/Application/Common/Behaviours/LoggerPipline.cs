﻿using ProductCatalogue.Application.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Common.Behaviours
{
    public class LoggerPipline<TRequest, TResponse> : IRequestPipeline<TRequest, TResponse>
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
        public async Task<Response<TResponse>> Handle(TRequest request, CancellationToken cancellationToken, MyRequestHandlerDelegate<TResponse> next)
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