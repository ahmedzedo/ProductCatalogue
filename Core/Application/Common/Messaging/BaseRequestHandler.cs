using ProductCatalogue.Application.Common.Interfaces.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Common.Messaging
{
    #region Abstract base Request Handler
    public abstract class AbstractBaseRequestHandler<TIn, TOut> : IBaseRequestHandler<TIn, TOut>
  where TIn : IBaseRequest<TOut>
    {
        #region Dependencies
        protected IServiceProvider ServiceProvider { get; }

        #endregion

        #region Constructor
        public AbstractBaseRequestHandler(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }
        #endregion

        #region Handel
        public virtual async Task<TOut> Handle(TIn request, CancellationToken cancellationToken)
        {
            return await HandleRequest(request, cancellationToken);
        }
        public abstract Task<TOut> HandleRequest(TIn request, CancellationToken cancellationToken);
        #endregion
    }
    #endregion

    #region Base Request Handler
    public abstract class BaseRequestHandler<TIn, TOut> : AbstractBaseRequestHandler<TIn, IResponse<TOut>>
   where TIn : Request<TOut>
    {
        #region Constructor
        public BaseRequestHandler(IServiceProvider serviceProvider)
           : base(serviceProvider)
        {

        }
        #endregion

        #region Helper Methods
        //private IEnumerable<T> GetInstances<T>() => (IEnumerable<T>)ServiceProvider.GetServices(typeof(T));

        //private async Task<IResponse<TOut>> GetResponse(TIn request, CancellationToken cancellationToken)
        //{
        //    var RequestPipelines = GetInstances<IRequestResponsePipeline<TIn, TOut>>();

        //    if (RequestPipelines.Any())
        //    {
        //        Task<IResponse<TOut>> Handler() => HandleRequest(request, cancellationToken);
        //        return await RequestPipelines
        //                .Reverse()
        //                .Aggregate((MyRequestResponseHandlerDelegate<TOut>)Handler, (next, pipeline) => () => pipeline.Handle(request, cancellationToken, next))();
        //    }
        //    else
        //    {
        //        return await HandleRequest(request, cancellationToken);
        //    }
        //} 
        #endregion

    }
    #endregion

    #region Base Commmand Handler
    public abstract class BaseCommandHandler<TIn, TOut> : BaseRequestHandler<TIn, TOut>
    where TIn : BaseCommand<TOut>
    {
        public BaseCommandHandler(IServiceProvider serviceProvider)
          : base(serviceProvider)
        {

        }
    }
    #endregion

    #region Base Query Handler
    public abstract class BaseQueryHandler<TIn, TOut> : BaseRequestHandler<TIn, TOut>
    where TIn : BaseQuery<TOut>
    {
        public BaseQueryHandler(IServiceProvider serviceProvider)
          : base(serviceProvider)
        {

        }
    } 
    #endregion


}
