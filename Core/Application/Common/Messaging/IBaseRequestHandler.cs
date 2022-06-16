using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Common.Messaging
{
    public interface IBaseRequestHandler<TIn, TOut> : IRequestHandler<TIn, TOut>
         where TIn : IBaseRequest<TOut>
    {
    }

}
