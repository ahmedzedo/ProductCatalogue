﻿namespace ProductCatalogue.Application.Common.Messaging
{
    public class Request<TResponse> : IBaseRequest<TResponse>
    {
        #region Properties
        public string UserName { get; set; }

        #endregion

        #region Constructors
        public Request(string userName = default)
        {
            UserName = userName;
        }
        #endregion
    }
    public class PagedListRequest<TResponse> : Request<TResponse>
    {
        #region Properites
        public int PageIndex { get; set; } = 0;

        public int PageSize { get; set; }

        public int PagePerPages { get; set; }
        #endregion

        #region Constructors
        public PagedListRequest(string userName = default)
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