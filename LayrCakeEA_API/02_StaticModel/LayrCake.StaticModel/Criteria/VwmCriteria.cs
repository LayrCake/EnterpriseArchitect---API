using System.Xml.Linq;
using Infrastructure.Criteria;
using Serialize.Linq.Serializers;

namespace LayrCake.StaticModel.Criteria
{
    // Render the class
    public partial class VwmCriteria : IVwmCriteria
    {
        internal ExpressionSerializer __expressionSerializer = new ExpressionSerializer(new XmlSerializer());

        public string Sort { get; set; }

        public Pagination Pagination { get; set; }

        public System.Xml.Linq.XElement WhereExpression { get; set; }

        public string WhereCondition { get; set; }
    }
}
