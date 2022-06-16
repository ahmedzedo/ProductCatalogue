using MediatR;

namespace ProductCatalogue.Application.Common.Messaging
{
    public interface IBaseRequest<T> : IRequest<T>
    {

    }

}
