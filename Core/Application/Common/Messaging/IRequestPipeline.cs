using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Common.Messaging
{
    public delegate Task<TResponse> MyRequestHandlerDelegate<TResponse>();
    public delegate Task<IResponse<TResponse>> MyRequestResponseHandlerDelegate<TResponse>();
    public interface IRequestPipeline<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, MyRequestHandlerDelegate<TResponse> next);
    }
    public interface IRequestResponsePipeline<TRequest, TResponse>
    {
        Task<IResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken, MyRequestResponseHandlerDelegate<TResponse> next);
    }
}
