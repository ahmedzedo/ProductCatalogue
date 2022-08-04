using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace ProductCatalogue.Presentation.WebUI.Common
{
    public class BasePageModel : PageModel
    {
        #region Dependencies
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        #endregion

        #region Constructors
        public BasePageModel()
        {

        }
        #endregion

        #region Properties

        #endregion
    }
}
