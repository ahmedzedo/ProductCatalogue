﻿using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.Common.Messaging;
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
    public class GetPagedProductQuery : PagedListRequest<IEnumerable<Product>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class GetPagedProductQueryHandler : BaseRequestHandler<GetPagedProductQuery, IEnumerable<Product>>
    {
        #region Dependencies
        private IProductRepository ProductRepository { get; set; }
        #endregion

        #region Constructor
        public GetPagedProductQueryHandler(IServiceProvider serviceProvider, IUnitOfWork unitOfWork, IProductRepository productRepository)
           : base(serviceProvider, unitOfWork)
        {
            ProductRepository = productRepository;
        }
        #endregion

        #region Handel
        public async override Task<IResponse<IEnumerable<Product>>> HandleRequest(GetPagedProductQuery request, CancellationToken cancellationToken)
        {
            (IEnumerable<Product> items, int totalCount) = await ProductRepository
                .GetQuery()
                .WhereIf(!string.IsNullOrEmpty(request.Name), p => p.Name.Contains(request.Name))
                .WhereIf(!string.IsNullOrEmpty(request.Description), p => p.Description.Contains(request.Description))
                .OrderBy(p => p.OrderByDescending(o => o.CreatedOn))
                .ToPagedListAsync(request.PageIndex, request.PageSize);
            Debug.WriteLine("in request");
            return Response.Success(items, totalCount);
        }
        #endregion
    }
}
