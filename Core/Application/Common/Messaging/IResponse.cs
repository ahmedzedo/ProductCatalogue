namespace ProductCatalogue.Application.Common.Messaging
{
    public interface IResponse<T>
    {
        T Data { get; set; }
        string Message { get; set; }
        bool IsSuccess { get; set; }

    }
}