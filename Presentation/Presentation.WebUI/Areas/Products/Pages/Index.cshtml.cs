using ProductCatalogue.Presentation.WebUI.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using ProductCatalogue.Application.ProductCatalogue.Queries.GetPagedProducts;
using ProductCatalogue.Application.ProductCatalogue.Commands.AddItemToCart;

namespace ProductCatalogue.Presentation.WebUI.Areas.Products.Pages
{
    public class IndexModel : BasePageModel
    {
        #region Dependencies
       
        #endregion

        #region Constuctors
        public IndexModel()
        {
           
        }
        #endregion

        #region Bind Properties
        [BindProperty]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public GetPagedProductQuery GetPagedProductQuery { get; set; } = new GetPagedProductQuery();
       [BindProperty]
        public AddItemToCartCommand AddItemToCartCommand { get; set; } = new AddItemToCartCommand();
        public IList<Product> Product { get; set; }
        #endregion

        #region Handlers
        public async Task OnGetAsync()
        {
            GetPagedProductQuery.PagePerPages = 3;
            GetPagedProductQuery.PageSize = 10;
            GetPagedProductQuery.UserName = HttpContext.User?.Identity.Name ?? "";
            var response = await Mediator.Send(GetPagedProductQuery);
            Product = response.Data.ToList();
            Message = response.Message;
        }

        public async Task<ActionResult> OnPostAddToCartAsync()
        {
            ModelState.Remove("PageIndex");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await Mediator.Send(AddItemToCartCommand);
            //Message = response.Message;

            return LocalRedirect("/Cart/CartView");//!response.IsSuccess ? Page() : LocalRedirect("/Cart/CartView");
        }
        #endregion
    }
}
