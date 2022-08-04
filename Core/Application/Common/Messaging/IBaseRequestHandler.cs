using MediatR;

namespace ProductCatalogue.Application.Common.Messaging
{
    public interface IBaseRequestHandler<TIn, TOut> : IRequestHandler<TIn, TOut>
         where TIn : IBaseRequest<TOut>
    {
    }

}
