using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.OData.Query;

namespace LayrCake.WebApi.Controllers
{
    public interface ITableController<TData>
    {
        IQueryable<TData> Query();

        Task<IEnumerable<TData>> QueryAsync(ODataQueryOptions query);
    }
}
