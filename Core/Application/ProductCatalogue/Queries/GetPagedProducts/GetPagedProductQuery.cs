using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.ProductCatalogue.Queries.GetPagedProducts
{
    #region Request
    public class GetPagedProductQuery : PagedListQuery<IEnumerable<GetPagedProductDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    #endregion

    #region Request Handler
    public class GetPagedProductQueryHandler : BaseQueryHandler<GetPagedProductQuery, IEnumerable<GetPagedProductDto>>
    {
        #region Dependencies

        #endregion

        #region Constructor
        public GetPagedProductQueryHandler(IServiceProvider serviceProvider, IApplicationDbContext dbContext)
           : base(serviceProvider, dbContext)
        {

        }
        #endregion

        #region Handel
        public override async Task<IResponse<IEnumerable<GetPagedProductDto>>> HandleRequest(GetPagedProductQuery request,
                                                                                  CancellationToken cancellationToken)
        {
            (IEnumerable<GetPagedProductDto> items, int totalCount) = await DbContext.ProductQuery
                .IncludeCartItems(request.UserName)
                .WhereIf(!string.IsNullOrEmpty(request.Name), p => p.Name.Contains(request.Name))
                .WhereIf(!string.IsNullOrEmpty(request.Description), p => p.Description.Contains(request.Description))
                .OrderByDescending(p => p.CreatedOn)
                .ToPagedListAsync<GetPagedProductDto>(request.PageIndex, request.PageSize,
                p => p);

            var x = DbContext.CartQuery.GetCartDetailes().OrderBy(s => s.CartId).FirstOrDefault(p => p.Count == 7);
            //var productDataQuery = DbContext.ProductQuery.Where(p => p.Name == "Product10");
            //productDataQuery = ((IProductDataQuery)productDataQuery).IncludeCartItems(request.UserName);
            //var pro = productDataQuery.FirstOrDefault();
            //var x = productDataQuery.FirstOrDefault(p => new GetPagedProductDto()
            //{
            //    Id = p.Id,
            //    Description = p.Description,
            //    Name = p.Name
            //});
            //var x1 = await DbContext.ProductQuery.ToListAsync<GetPagedProductDto>(p => p);
            //Debug.WriteLine("in request");

            return Response.Success(items, totalCount);
        }
        #endregion
    }
    #endregion
}
