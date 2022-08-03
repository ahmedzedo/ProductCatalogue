using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.Common.Messaging;
using ProductCatalogue.Application.ProductCatalogue.IDataQueries;
using ProductCatalogue.Application.ProductCatalogue.IRepositories;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.ProductCatalogue.Queries.GetPagedProducts
{
    #region Request
    public class GetPagedProductQuery : PagedListQuery<IEnumerable<Product>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    #endregion

    #region Request Handler
    public class GetPagedProductQueryHandler : BaseQueryHandler<GetPagedProductQuery, IEnumerable<Product>>
    {
        #region Dependencies

       // public IProductDataQuery ProductDataQuery => (IProductDataQuery)ServiceProvider.GetService(typeof(IProductDataQuery));
        public IApplicationDbContext DbContext => (IApplicationDbContext)ServiceProvider.GetService(typeof(IApplicationDbContext));
        #endregion

        #region Constructor
        public GetPagedProductQueryHandler(IServiceProvider serviceProvider)
           : base(serviceProvider)
        {

        }
        #endregion

        #region Handel
        public async override Task<IResponse<IEnumerable<Product>>> HandleRequest(GetPagedProductQuery request, CancellationToken cancellationToken)
        {
            (IEnumerable<Product> items, int totalCount) = await DbContext.ProductQuery
                .IncludeCartItems()
                .WhereIf(!string.IsNullOrEmpty(request.Name), p => p.Name.Contains(request.Name))
                .WhereIf(!string.IsNullOrEmpty(request.Description), p => p.Description.Contains(request.Description))
                .OrderBy(p => p.OrderByDescending(o => o.CreatedOn))
                .ToPagedListAsync(request.PageIndex, request.PageSize);

            IProductDataQuery productDataQuery = DbContext.ProductQuery;
         var x =   productDataQuery.Where(p => p.Name == "product1")
                            .FirstOrDefault();

            Debug.WriteLine("in request");

            return Response.Success(items, totalCount);
        }
        #endregion
    }
    #endregion
}
