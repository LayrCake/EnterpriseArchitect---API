using System.Xml.Linq;
using Infrastructure.Criteria;

namespace LayrCake.StaticModel.Criteria
{
    /// <summary>
    /// Base class that holds criteria for queries.
    /// </summary>
    public interface IVwmCriteria
    {
        /// <summary>
        /// Sort expression of the criteria.
        /// </summary>
        string Sort { get; set; }

        /// <summary>
        /// Pagination criteria for GetAll requests
        /// </summary>
        Pagination Pagination { get; set; }

        XElement WhereExpression { get; set; }

        string WhereCondition { get; set; }
    }
}
