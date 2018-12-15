using LayrCake.StaticModel.Repositories;
using LayrCake.WebApi.Controllers.Overrides;
using StaticModel.ViewModelObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
//using System.Web.Http.OData;
//using System.Web.Http.OData.Query;
using AutoMapper;
using AutoMapper.Internal;

namespace LayrCake.WebApi.Controllers
{
    // Summary:
    //     Provides a common System.Web.Http.ApiController abstraction for Table Controllers.
    //
    // Type parameters:
    //   TData:
    //     The type of the entity.
    public abstract class TableController<TData> : TableController where TData : class 
       // , ViewModelObject, new() where TModel : class
    {
        //// Summary:
        ////     Initializes a new instance of the Microsoft.Azure.Mobile.Server.TableController<TData>
        ////     class.
        //protected TableController();
        ////
        //// Summary:
        ////     Initializes a new instance of the Microsoft.Azure.Mobile.Server.TableController<TData>
        ////     class with a given RepositoryManager.
        ////
        //// Parameters:
        ////   RepositoryManager:
        ////     The Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData> for this controller.
        //protected TableController(IRepository<TData> RepositoryManager);

        //// Summary:
        ////     Gets or sets the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>
        ////     to be used for accessing the backend store.
        //protected IRepository<TData> RepositoryManager { get; set; }

        //// Summary:
        ////     Provides a helper method for deleting an entity from a backend store. It
        ////     deals with any exceptions thrown by the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>
        ////     and maps them into appropriate HTTP responses.
        ////
        //// Returns:
        ////     A System.Threading.Tasks.Task<TResult> representing the delete operation
        ////     executed by the the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>.
        //[DebuggerStepThrough]
        //[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Response is disposed in the response path.")]
        //protected virtual Task DeleteAsync(string id);
        ////
        //protected override void Initialize(HttpControllerContext controllerContext);
        ////
        //// Summary:
        ////     Provides a helper method for inserting an entity into a backend store. It
        ////     deals with any model validation errors as well as exceptions thrown by the
        ////     Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData> and maps them
        ////     into appropriate HTTP responses.
        ////
        //// Returns:
        ////     A System.Threading.Tasks.Task<TResult> representing the insert operation
        ////     executed by the the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>.
        //[DebuggerStepThrough]
        //[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Response is disposed in the response path.")]
        //protected virtual Task<TData> InsertAsync(TData item);
        ////
        //// Summary:
        ////     Provides a helper method for looking up an entity in a backend store. It
        ////     deals with any exceptions thrown by the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>
        ////     and maps them into appropriate HTTP responses.
        ////
        //// Returns:
        ////     An System.Web.Http.SingleResult<T> returned by the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>.
        //[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Caller disposes response.")]
        //protected virtual SingleResult<TData> Lookup(string id);
        ////
        //// Summary:
        ////     Provides a helper method for looking up an entity in a backend store. It
        ////     deals with any exceptions thrown by the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>
        ////     and maps them into appropriate HTTP responses.
        ////
        //// Returns:
        ////     An System.Web.Http.SingleResult<T> returned by the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>.
        //[DebuggerStepThrough]
        //[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Caller disposes response.")]
        //protected virtual Task<SingleResult<TData>> LookupAsync(string id);
        ////
        //// Summary:
        ////     Provides a helper method for querying a backend store. It deals with any
        ////     exceptions thrown by the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>
        ////     and maps them into appropriate HTTP responses.
        ////
        //// Returns:
        ////     An System.Linq.IQueryable<T> returned by the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>.
        //[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Caller disposes response.")]
        //protected virtual IQueryable<TData> Query();
        ////
        //// Summary:
        ////     Provides a helper method for querying a backend store. It deals with any
        ////     exceptions thrown by the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>
        ////     and maps them into appropriate HTTP responses.
        ////
        //// Returns:
        ////     An System.Linq.IQueryable<T> returned by the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>.
        //protected virtual Task<System.Collections.Generic.IEnumerable<TData>> QueryAsync(ODataQueryOptions query);
        ////
        //// Summary:
        ////     Provides a helper method for replacing an entity in a backend store. It deals
        ////     with any model validation errors as well as exceptions thrown by the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>
        ////     and maps them into appropriate HTTP responses.
        ////
        //// Returns:
        ////     A System.Threading.Tasks.Task<TResult> representing the replace operation
        ////     executed by the the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>.
        //[DebuggerStepThrough]
        //[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Response is disposed in the response path.")]
        //protected virtual Task<TData> ReplaceAsync(string id, TData item);
        ////
        //// Summary:
        ////     Provides a helper method for undeleting an entity in a backend store. It
        ////     deals with any model validation errors as well as exceptions thrown by the
        ////     Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData> and maps them
        ////     into appropriate HTTP responses.
        ////
        //// Returns:
        ////     A System.Threading.Tasks.Task<TResult> representing the update operation
        ////     executed by the the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>.
        //protected virtual Task<TData> UndeleteAsync(string id);
        ////
        //// Summary:
        ////     Provides a helper method for undeleting an entity in a backend store. It
        ////     deals with any model validation errors as well as exceptions thrown by the
        ////     Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData> and maps them
        ////     into appropriate HTTP responses.
        ////
        //// Returns:
        ////     A System.Threading.Tasks.Task<TResult> representing the undelete operation
        ////     executed by the the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>.
        //[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Response is disposed in the response path.")]
        //protected virtual Task<TData> UndeleteAsync(string id, Delta<TData> patch);
        ////
        //// Summary:
        ////     Provides a helper method for updating an entity in a backend store. It deals
        ////     with any model validation errors as well as exceptions thrown by the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>
        ////     and maps them into appropriate HTTP responses.
        ////
        //// Returns:
        ////     A System.Threading.Tasks.Task<TResult> representing the update operation
        ////     executed by the the Microsoft.Azure.Mobile.Server.Tables.IRepositoryManager<TData>.
        //protected virtual Task<TData> UpdateAsync(string id, Delta<TData> patch);

    //private static Expression<Func<TModel, bool>> GeneratePredicate(string id) {
    //    var m = Mapper.FindTypeMapFor<TModel, TData>();
    //    var pmForId = m.GetExistingPropertyMapFor(new PropertyAccessor(typeof(TData).GetProperty("Id")));
    //    var keyString = pmForId.CustomExpression;
    //    var predicate = Expression.Lambda<Func<TModel, bool>>(
    //        Expression.Equal(keyString.Body, Expression.Constant(id)),
    //        keyString.Parameters[0]);
    //    return predicate;
    //}
    //private object ConvertId(string id) {
    //    var m = Mapper.FindTypeMapFor<TData, TModel>();
    //    var keyPropertyAccessor = GetPropertyAccessor(this.dbKeyProperty);
    //    var pmForId = m.GetExistingPropertyMapFor(new PropertyAccessor(keyPropertyAccessor));
    //    TData tmp = new TData() { ID = id };
    //    var convertedId = pmForId.CustomExpression.Compile().DynamicInvoke(tmp);
    //    return convertedId;
    //}
    //private PropertyInfo GetPropertyAccessor(Expression exp) {
    //    if (exp.NodeType == ExpressionType.Lambda) {
    //        var lambda = exp as LambdaExpression;
    //        return GetPropertyAccessor(lambda.Body);
    //    } else if (exp.NodeType == ExpressionType.Convert) {
    //        var convert = exp as UnaryExpression;
    //        return GetPropertyAccessor(convert.Operand);
    //    } else if (exp.NodeType == ExpressionType.MemberAccess) {
    //        var propExp = exp as System.Linq.Expressions.MemberExpression;
    //        return propExp.Member as PropertyInfo;
    //    } else {
    //        throw new InvalidOperationException("Unexpected expression node type: " + exp.NodeType);
    //    }
    }
}