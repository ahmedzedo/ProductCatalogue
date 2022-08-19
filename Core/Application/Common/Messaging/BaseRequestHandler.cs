using ProductCatalogue.Application.Common.Interfaces.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Common.Messaging
{
    #region Class BaseRequestHandler
    public abstract class BaseRequestHandler<TIn, TOut> : IBaseRequestHandler<TIn, TOut>
     where TIn : IBaseRequest<TOut>
    {
        #region Dependencies
        protected IServiceProvider ServiceProvider { get; }

        #endregion

        #region Constructor
        public BaseRequestHandler(IServiceProvider serviceProvider)
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

    #region Class AppRequestHandler
    public abstract class AppRequestHandler<TIn, TOut> : BaseRequestHandler<TIn, IResponse<TOut>>
   where TIn : AppRequest<TOut>
    {
        #region Dependencies
        protected IApplicationDbContext DbContext { get; }
        #endregion

        #region Constructor
        public AppRequestHandler(IServiceProvider serviceProvider, IApplicationDbContext dbContext)
           : base(serviceProvider)
        {
            DbContext = dbContext;
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

    #region Class BaseCommandHandler
    public abstract class BaseCommandHandler<TIn, TOut> : AppRequestHandler<TIn, TOut>
    where TIn : BaseCommand<TOut>
    {
        public BaseCommandHandler(IServiceProvider serviceProvider, IApplicationDbContext dbContext)
          : base(serviceProvider, dbContext)
        {

        }
    }
    #endregion

    #region Class BaseQueryHandler
    public abstract class BaseQueryHandler<TIn, TOut> : AppRequestHandler<TIn, TOut>
    where TIn : BaseQuery<TOut>
    {
        public BaseQueryHandler(IServiceProvider serviceProvider, IApplicationDbContext dbContext)
          : base(serviceProvider, dbContext)
        {

        }
    }
    #endregion


}
