using LayrCake.StaticModel.Criteria;
using LayrCake.StaticModel.Repositories;
using LayrCake.WebApi.Models;
using LayrCake.WebApi.Properties;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.Tracing;

namespace LayrCake.WebApi.Controllers
{
    /// <summary>
    /// Provides a common <see cref="ApiController"/> abstraction for Table Controllers.
    /// </summary>
    /// <typeparam name="TData">The type of the ApiModel.</typeparam>
    public abstract class AzureTableController<TData> : TableController,
        ITableController<TData> where TData : class //IApiModelObject
    {
        private IRepository<TData> repositoryManager;
        internal ITraceWriter traceWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableController{TData}"/> class.
        /// </summary>
        protected AzureTableController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableController{TData}"/> class
        /// with a given <paramref name="repositoryManager"/>.
        /// </summary>
        /// <param name="repositoryManager">The <see cref="IRepositoryManager{T}"/> for this controller.</param>
        protected AzureTableController(IRepository<TData> repositoryManager)
        {
            if (repositoryManager == null)
            {
                throw new ArgumentNullException("repositoryManager");
            }

            this.RepositoryManager = repositoryManager;
        }

        /// <summary>
        /// Gets or sets the <see cref="IRepositoryManager{TData}"/> to be used for accessing the backend store.
        /// </summary>
        protected IRepository<TData> RepositoryManager
        {
            get
            {
                if (this.repositoryManager == null)
                {
                    string msg = "";// TResources.TableController_NoRepositoryManager.FormatForUser("IRepository", typeof(IRepository<>).GetShortName());
                    throw new InvalidOperationException(msg);
                }

                return this.repositoryManager;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                this.repositoryManager = value;
            }
        }

        /// <inheritdoc />
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            this.traceWriter = this.Configuration.Services.GetTraceWriter();
        }

        /// <summary>
        /// Provides a helper method for querying a backend store. It deals with any exceptions thrown by the <see cref="IRepositoryManager{TData}"/>
        /// and maps them into appropriate HTTP responses.
        /// </summary>
        /// <returns>An <see cref="IQueryable{TData}"/> returned by the <see cref="IRepositoryManager{TData}"/>.</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Caller disposes response.")]
        public virtual IQueryable<TData> Query()
        {
            try
            {
                return this.RepositoryManager.GetList().AsQueryable();
            }
            catch (HttpResponseException ex)
            {
                this.traceWriter.Error(ex, this.Request, LogCategories.TableControllers);
                throw;
            }
            catch (Exception ex)
            {
                this.traceWriter.Error(ex, this.Request, LogCategories.TableControllers);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        /// <summary>
        /// Provides a helper method for querying a backend store. It deals with any exceptions thrown by the <see cref="IRepositoryManager{TData}"/>
        /// and maps them into appropriate HTTP responses.
        /// </summary>
        /// <returns>An <see cref="IQueryable{TData}"/> returned by the <see cref="IRepositoryManager{TData}"/>.</returns>
        public virtual Task<IEnumerable<TData>> QueryAsync(ODataQueryOptions query)
        {
            var top = 0;
            int.TryParse(query.Top.RawValue, out top);
            //query.Skip
            //queryable = query.ApplyTo(this.RepositoryManager.GetList().AsQueryable(), new ODataQuerySettings());

            //Expression<Func<TData, bool>> myExpression = query.Filter.ToExpression<TData>();

            var result = this.RepositoryManager.GetList().AsQueryable();

            IVwmCriteria criteria = new VwmCriteria();
            criteria.Pagination = new Infrastructure.Criteria.Pagination()
            {
                PageSize = top
            };

            return Task.Run(() => this.RepositoryManager.GetList(criteria).AsEnumerable());
        }

        /// <summary>
        /// Provides a helper method for looking up an entity in a backend store. It deals with any exceptions thrown by the <see cref="IRepositoryManager{TData}"/>
        /// and maps them into appropriate HTTP responses.
        /// </summary>
        /// <returns>An <see cref="SingleResult{TData}"/> returned by the <see cref="IRepositoryManager{TData}"/>.</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Caller disposes response.")]
        protected virtual SingleResult<TData> Lookup(string id)
        {
            try
            {
                int Id = 0;
                int.TryParse(id, out Id);

                return this.RepositoryManager.Get(Id) as SingleResult<TData>;
            }
            catch (HttpResponseException ex)
            {
                this.traceWriter.Error(ex, this.Request, LogCategories.TableControllers);
                throw;
            }
            catch (Exception ex)
            {
                this.traceWriter.Error(ex, this.Request, LogCategories.TableControllers);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        /// <summary>
        /// Provides a helper method for looking up an entity in a backend store. It deals with any exceptions thrown by the <see cref="IRepositoryManager{TData}"/>
        /// and maps them into appropriate HTTP responses.
        /// </summary>
        /// <returns>An <see cref="SingleResult{TData}"/> returned by the <see cref="IRepositoryManager{TData}"/>.</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Caller disposes response.")]
        protected virtual async Task<SingleResult<TData>> LookupAsync(string id)
        {
            try
            {
                int Id = 0;
                int.TryParse(id, out Id);

                return this.RepositoryManager.Get(Id) as SingleResult<TData>;
            }
            catch (HttpResponseException ex)
            {
                this.traceWriter.Error(ex, this.Request, LogCategories.TableControllers);
                throw;
            }
            catch (Exception ex)
            {
                this.traceWriter.Error(ex, this.Request, LogCategories.TableControllers);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        /// <summary>
        /// Provides a helper method for inserting an entity into a backend store. It deals with any model validation errors as well as
        /// exceptions thrown by the <see cref="IRepositoryManager{TData}"/> and maps them into appropriate HTTP responses.
        /// </summary>
        /// <returns>A <see cref="Task{TData}"/> representing the insert operation executed by the the <see cref="IRepositoryManager{TData}"/>.</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Response is disposed in the response path.")]
        protected async virtual Task<TData> InsertAsync(TData item)
        {
            if (item == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, TResources.TableController_NullRequestBody));
            }

            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            }

            try
            {
                return await Task.Run(() => this.RepositoryManager.Insert(item));
            }
            catch (HttpResponseException ex)
            {
                this.traceWriter.Error(ex, this.Request, LogCategories.TableControllers);
                throw;
            }
            catch (Exception ex)
            {
                this.traceWriter.Error(ex, this.Request, LogCategories.TableControllers);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        /// <summary>
        /// Provides a helper method for updating an entity in a backend store. It deals with any model validation errors as well as
        /// exceptions thrown by the <see cref="IRepositoryManager{TData}"/> and maps them into appropriate HTTP responses.
        /// </summary>
        /// <returns>A <see cref="Task{TData}"/> representing the update operation executed by the the <see cref="IRepositoryManager{TData}"/>.</returns>
        protected virtual Task<TData> UpdateAsync(string id, Delta<TData> patch)
        {
            var item = patch.GetEntity();
            return Task.Run(() => this.RepositoryManager.Update(item));

            //return this.RepositoryManager.Update(entity);
            //return null; // this.PatchAsync(id, patch, (i, p) => this.RepositoryManager.UpdateAsync(i, p));
        }

        /// <summary>
        /// Provides a helper method for undeleting an entity in a backend store. It deals with any model validation errors as well as
        /// exceptions thrown by the <see cref="IRepositoryManager{TData}"/> and maps them into appropriate HTTP responses.
        /// </summary>
        /// <returns>A <see cref="Task{TData}"/> representing the update operation executed by the the <see cref="IRepositoryManager{TData}"/>.</returns>
        protected virtual Task<TData> UndeleteAsync(string id)
        {
            return this.UndeleteAsync(id, new Delta<TData>());
        }

        /// <summary>
        /// Provides a helper method for undeleting an entity in a backend store. It deals with any model validation errors as well as
        /// exceptions thrown by the <see cref="IRepositoryManager{TData}"/> and maps them into appropriate HTTP responses.
        /// </summary>
        /// <returns>A <see cref="Task{TData}"/> representing the undelete operation executed by the the <see cref="IRepositoryManager{TData}"/>.</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Response is disposed in the response path.")]
        protected virtual Task<TData> UndeleteAsync(string id, Delta<TData> patch)
        {
            var manager = this.RepositoryManager as IRepository<TData>;
            if (manager == null)
            {
                throw new NotSupportedException(TResources.RepositoryManager_DoesNotSupportSoftDelete);
            }

            if (patch == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, TResources.TableController_NullRequestBody));
            }
            var item = patch.GetEntity();
            return Task.Run(() => this.RepositoryManager.Update(item));
            //return null; // this.PatchAsync(id, patch, (i, p) => manager.UndeleteAsync(i, p));
        }

        /// <summary>
        /// Provides a helper method for replacing an entity in a backend store. It deals with any model validation errors as well as
        /// exceptions thrown by the <see cref="IRepositoryManager{TData}"/> and maps them into appropriate HTTP responses.
        /// </summary>
        /// <returns>A <see cref="Task{TData}"/> representing the replace operation executed by the the <see cref="IRepositoryManager{TData}"/>.</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Response is disposed in the response path.")]
        protected async virtual Task<TData> ReplaceAsync(string id, TData item)
        {
            if (item == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, TResources.TableController_NullRequestBody));
            }

            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            }

            // Add ETag from request If-Match header
            byte[] version = this.Request.GetVersionFromIfMatch();
            if (version != null)
            {
                //item.Version = version;
            }

            try
            {
                return await Task.Run(() => this.RepositoryManager.Update(item));
            }
            catch (HttpResponseException ex)
            {
                this.traceWriter.Error(ex, this.Request, LogCategories.TableControllers);
                throw;
            }
            catch (Exception ex)
            {
                this.traceWriter.Error(ex, this.Request, LogCategories.TableControllers);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        /// <summary>
        /// Provides a helper method for deleting an entity from a backend store. It deals with any
        /// exceptions thrown by the <see cref="IRepositoryManager{TData}"/> and maps them into appropriate HTTP responses.
        /// </summary>
        /// <returns>A <see cref="Task{TData}"/> representing the delete operation executed by the the <see cref="IRepositoryManager{TData}"/>.</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Response is disposed in the response path.")]
        protected virtual async Task DeleteAsync(string id)
        {
            bool result = false;
            try
            {
                var resultItem = this.RepositoryManager.Delete(Int32.Parse(id));
                result = true;
                //result = await this.RepositoryManager.DeleteAsync(id);
            }
            catch (HttpResponseException ex)
            {
                this.traceWriter.Error(ex, this.Request, LogCategories.TableControllers);
                throw;
            }
            catch (Exception ex)
            {
                this.traceWriter.Error(ex, this.Request, LogCategories.TableControllers);
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }

            if (!result)
            {
                throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.NotFound));
            }
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Response is disposed in the response path.")]
        private async Task<TData> PatchAsync(string id, Delta<TData> patch, Func<string, Delta<TData>, Task<TData>> patchAction)
        {
            if (patch == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, TResources.TableController_NullRequestBody));
            }

            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            }

            // Add ETag from request If-Match header
            byte[] version = this.Request.GetVersionFromIfMatch();
            if (version != null)
            {
                if (!patch.TrySetPropertyValue(TableUtils.VersionPropertyName, version))
                {
                    //string error = TResources.TableController_CouldNotSetVersion.FormatForUser(TableUtils.VersionPropertyName, version);
                    //this.traceWriter.Error(error, this.Request, ServiceLogCategories.TableControllers);
                    //throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, error));
                }
            }

            try
            {
                return await patchAction(id, patch);
            }
            catch (HttpResponseException ex)
            {
                this.traceWriter.Error(ex, this.Request, LogCategories.TableControllers);
                throw;
            }
            catch (Exception ex)
            {
                this.traceWriter.Error(ex, this.Request, LogCategories.TableControllers);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }


        //protected virtual int BeforeFetch(IQueryable<TData> data)
        //{

        //}

    }
}

