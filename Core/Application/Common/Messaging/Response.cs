using ProductCatalogue.Application.Common.Models;

namespace ProductCatalogue.Application.Common.Messaging
{
    public static class Response
    {
        #region Static Methods
        public static Response<T> Failuer<T>(string message = "Failuer", Errors errors = default)
        {
            return new Response<T>(message, false, errors);
        }

        public static Response<T> Success<T>(T data = default, int count = 0, string message = "OK")
        {
            return new Response<T>(data, count, message, true, default);
        }

        #endregion
    }

    public class Response<T> : IResponse<T>
    {
        #region Public Properties

        public T Data { get; set; }
        public int Count { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Errors Errors { get; set; }
        #endregion

        #region Constructors
        public Response(string message, bool isSuccess, Errors errors) : this(default, 0, message, isSuccess, errors)
        {
        }
        public Response(T data, int count, string message, bool isSuccess, Errors errors)
        {
            Data = data;
            Count = count;
            Message = message;
            IsSuccess = isSuccess;
            Errors = errors;
        }


        #endregion
    }
}
