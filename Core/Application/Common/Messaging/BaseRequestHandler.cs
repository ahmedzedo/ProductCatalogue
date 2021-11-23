using ProductCatalogue.Application.Common.Interfaces.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProductCatalogue.Application.ProductCatalogue.IRepositories;

namespace ProductCatalogue.Application.Common.Messaging
{
    public abstract class BaseRequestHandler<TIn, TOut> : IBaseRequestHandler<TIn, TOut> where TIn : IBaseRequest<TOut>
    {
        #region Dependencies
        public IUnitOfWork UnitOfWork { get; }
      

        protected IServiceProvider ServiceProvider { get; }

        private IRequestPipeline<TIn, TOut> _requestPipeline;
        protected IRequestPipeline<TIn, TOut> RequestPipeline => _requestPipeline ??= ServiceProvider.GetRequiredService<IRequestPipeline<TIn, TOut>>();
        #endregion

        #region Constructor
        public BaseRequestHandler(IServiceProvider serviceProvider, IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
           
            this.ServiceProvider = serviceProvider;
        }
        #endregion

        #region Handel
        public virtual async Task<Response<TOut>> Handle(TIn request, CancellationToken cancellationToken)
        {
            Response<TOut> response = null;

            if (RequestPipeline != null)
            {
                response = await RequestPipeline.Handle(request, HandleRequest, cancellationToken);
            }
            else
            {
                response = await HandleRequest(request, cancellationToken);
            }

            return response;
        }
        public abstract Task<Response<TOut>> HandleRequest(TIn request, CancellationToken cancellationToken);

        #endregion


    }
}
