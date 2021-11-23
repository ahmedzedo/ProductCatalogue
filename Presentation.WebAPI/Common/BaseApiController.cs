using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.WebAPI.Common
{
    public class BaseApiController : ControllerBase
    {
        #region Dependencies
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        #endregion

        #region Constructors
        public BaseApiController()
        {

        }
        #endregion

        #region Properties

        #endregion
    }
}
