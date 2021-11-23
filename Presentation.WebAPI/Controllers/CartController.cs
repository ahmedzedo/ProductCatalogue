using Microsoft.AspNetCore.Mvc;
using Presentation.WebAPI.Common;
using ProductCatalogue.Application.ProductCatalogue.Commands.AddItemToCart;
using ProductCatalogue.Application.ProductCatalogue.Commands.RemoveCartItem;
using ProductCatalogue.Application.ProductCatalogue.Commands.UpdateItemToCart;
using ProductCatalogue.Application.ProductCatalogue.Queries.GetCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.WebAPI.Controllers
{
    public class CartController : BaseApiController
    {
        [HttpPost("get-Cart")]
        public async Task<IActionResult> GetCart([FromBody] GetCartQuery model)
        {
            var response = await Mediator.Send(model);
            return Ok(response);
        }
        [HttpPost("add-item-to-cart")]
        public async Task<IActionResult> AddItemToCart([FromBody] AddItemToCartCommand model)
        {
            var response = await Mediator.Send(model);
            return Ok(response);
        }

        [HttpPost("edit-item-count")]
        public async Task<IActionResult> EditItemCount([FromBody] UpdateCartItemCommand model)
        {
            var response = await Mediator.Send(model);
            return Ok(response);
        }
        [HttpPost("remove-item")]
        public async Task<IActionResult> RemoveCartItem([FromBody] RemoveCartItemCommand model)
        {
            var response = await Mediator.Send(model);
            return Ok(response);
        }
    }
}
