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
        private  IEnumerable<T> GetInstances<T>()
        => (IEnumerable<T>)ServiceProvider.GetServices(typeof(T));
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
                Task<Response<TOut>> Handler()=> HandleRequest(request,cancellationToken);
                response =  GetInstances<IRequestPipeline<TIn, TOut>>()
            .Reverse()
            .Aggregate((MyRequestHandlerDelegate<TOut>)Handler, (next, pipeline) => () => pipeline.Handle(request, cancellationToken, next))().Result;
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
