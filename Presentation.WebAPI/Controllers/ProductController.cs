using Microsoft.AspNetCore.Mvc;
using Presentation.WebAPI.Common;
using ProductCatalogue.Application.ProductCatalogue.Queries.GetPagedProducts;
using System.Threading.Tasks;

namespace Presentation.WebAPI.Controllers
{
    public class ProductController : BaseApiController
    {
        // GET: ProductController
        [HttpPost("get-product-list")]
        public async Task<IActionResult> GetProductList([FromBody] GetPagedProductQuery model)
        {
            var response = await Mediator.Send(model);
            return Ok(response);
        }

    }
}