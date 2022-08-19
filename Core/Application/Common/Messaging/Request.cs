namespace ProductCatalogue.Application.Common.Messaging
{
    #region Class BaseRequest 
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
    #endregion

    #region Class AppRequest
    public class AppRequest<TResponse> : BaseRequest<IResponse<TResponse>>
    {
        #region Constructor
        public AppRequest(string userName = default)
             : base(userName)
        {

        }
        #endregion
    }
    #endregion

    #region Class BaseCommand
    public class BaseCommand<TResponse> : AppRequest<TResponse>
    {
        #region Constructor
        public BaseCommand(string userName = default)
           : base(userName)
        {

        }
        #endregion
    }
    #endregion

    #region Class BaseQuery
    public class BaseQuery<TResponse> : AppRequest<TResponse>
    {
        #region Constructor
        public BaseQuery(string userName = default)
             : base(userName)
        {

        }
        #endregion
    }
    #endregion

    #region Class PagedListQuery
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

        #endregion
    }
    #endregion
}