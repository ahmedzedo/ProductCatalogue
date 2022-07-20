namespace ProductCatalogue.Application.Common.Messaging
{
     public class BaseRequest<TResponse> : IBaseRequest<TResponse>
    {
        #region Properties
        public string UserName { get; set; }

        #endregion

        #region Constructors
        public BaseRequest(string userName = default)
        {
            UserName = userName;
        }
        #endregion
    }
    
    public class Request<TResponse> : BaseRequest<IResponse<TResponse>>
    {
        public Request(string userName = default)
             : base(userName)
        {

        }
    }

    public class BaseCommand<TResponse> : Request<TResponse>
    {
        public BaseCommand(string userName = default)
             : base(userName)
        {

        }
    }
    public class BaseQuery<TResponse> : Request<TResponse>
    {
        public BaseQuery(string userName = default)
             : base(userName)
        {

        }
    }

    public class PagedListQuery<TResponse> : BaseQuery<TResponse>
    {
        #region Properites
        public int PageIndex { get; set; } = 0;

        public int PageSize { get; set; }

        public int PagePerPages { get; set; }
        #endregion

        #region Constructors
        public PagedListQuery(string userName = default)
            : base(userName)
        {

        }
        //public PagedListRequest(int pageSize = 10, int pagePerPages = 10, string userName = default)
        //    : base(userName)
        //{
        //    this.PageSize = pageSize;
        //    this.PagePerPages = pagePerPages;
        //}
        #endregion
    }
}