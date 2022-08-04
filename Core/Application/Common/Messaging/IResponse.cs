using ProductCatalogue.Application.Common.Models;

namespace ProductCatalogue.Application.Common.Messaging
{
    public interface IResponse<T>
    {
        T Data { get; set; }
        int Count { get; set; }
        bool IsSuccess { get; set; }
        string Message { get; set; }
        Errors Errors { get; set; }


    }
}