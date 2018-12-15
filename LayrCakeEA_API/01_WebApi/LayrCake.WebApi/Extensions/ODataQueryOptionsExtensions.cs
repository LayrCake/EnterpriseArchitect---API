using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http.OData.Query;

namespace LayrCake.WebApi
{
    public static class ODataQueryOptionsExtensions
    {
        public static Expression ToExpression<TElement>(this FilterQueryOption filter)
        {
            //IQueryable queryable = Enumerable.Empty<TElement>().AsQueryable();
            //queryable = filter.ApplyTo(queryable, new ODataQuerySettings());
            //return queryable.Expression;
            var enumerable = Enumerable.Empty<TElement>().AsQueryable();

            var param = Expression.Parameter(typeof(TElement));
            if (filter != null)
            {
                enumerable = (IQueryable<TElement>)filter.ApplyTo(enumerable, new ODataQuerySettings());

                var mce = enumerable.Expression as MethodCallExpression;
                if (mce != null)
                {
                    var quote = mce.Arguments[1] as UnaryExpression;
                    if (quote != null)
                    {
                        return quote.Operand as Expression<Func<TElement, bool>>;
                    }
                }
            }
            return Expression.Lambda<Func<TElement, bool>>(Expression.Constant(true), param);
        }
    }
}