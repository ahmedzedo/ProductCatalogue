using ProductCatalogue.Presentation.WebUI.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using ProductCatalogue.Application.ProductCatalogue.Queries.GetPagedProducts;
using ProductCatalogue.Application.ProductCatalogue.Queries.GetCart;
using System;
using ProductCatalogue.Application.ProductCatalogue.Commands.RemoveCartItem;
using ProductCatalogue.Application.ProductCatalogue.Commands.UpdateCartItem;

namespace ProductCatalogue.Presentation.WebUI.Areas.Cart.Pages
{
    public class CartViewModel : BasePageModel
    {
        #region Dependencies

        #endregion

        #region Constuctors
        public CartViewModel()
        {

        }
        #endregion

        #region Bind Properties
        [BindProperty]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public GetCartQuery GetCartQuery { get; set; } = new GetCartQuery();

        [BindProperty]
        public IList<CartItem> CartItems { get; set; } = new List<CartItem>();
        #endregion

        #region Handlers
        public async Task OnGetAsync()
        {
            var response = await Mediator.Send(GetCartQuery);

            if (response == null)
            {
                throw new Exception("Unexpected Exception");
            }

            if (response.IsSuccess)
            {
                CartItems = response.Data.Items.ToList();
            }
            Message = response.Message;
        }

        public async Task<ActionResult> OnPostEdit(Guid id)
        {
            var updateUpdateCartItemCommand = new UpdateCartItemCommand
            {
                Id = id,
                Count = CartItems.FirstOrDefault(c => c.Id == id).Count
            };
            var response = await Mediator.Send(updateUpdateCartItemCommand);
            Message = response.Message;

            return Redirect("./CartView");
        }
        public async Task<ActionResult> OnPostDelete(Guid id)
        {
            var removeCartItemCommand = new RemoveCartItemCommand
            {
                Id = id,
            };
            var response = await Mediator.Send(removeCartItemCommand);
            Message = response.Message;

            return Redirect("./CartView");
        }
        #endregion
    }
}
