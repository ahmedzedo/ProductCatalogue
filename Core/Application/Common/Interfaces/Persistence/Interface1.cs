using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Common.Interfaces.Persistence
{
    internal interface IDataQueryFactory
    {
        internal static TDataQuery GetDataQuery<TDataQuery>(IServiceProvider serviceProvider)
        {
            return (TDataQuery)serviceProvider.GetService(typeof(TDataQuery));
        }
    }
}
