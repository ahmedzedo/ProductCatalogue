using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Common.Messaging
{
    public delegate Task<TResponse> MyRequestHandlerDelegate<TResponse>();
    public delegate Task<IResponse<TResponse>> MyRequestResponseHandlerDelegate<TResponse>();
    public interface IRequestPipeline<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request,
                               MyRequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);
    }
    public interface IRequestResponsePipeline<TRequest, TResponse>
    {
        Task<IResponse<TResponse>> Handle(TRequest request,
                                          MyRequestResponseHandlerDelegate<TResponse> next, CancellationToken cancellationToken);
    }
}
