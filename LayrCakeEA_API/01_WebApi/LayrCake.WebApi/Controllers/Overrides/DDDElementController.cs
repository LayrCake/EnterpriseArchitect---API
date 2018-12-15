using LayrCake.StaticModel.Criteria.Implementation;
using LayrCake.StaticModel.ViewModelObjects.Implementation;
using LayrCake.WebApi.ModelMapper;
using LayrCake.WebApi.Models.Implementation;
using Microsoft.Azure.Mobile.Server;
using NCommon.Specifications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace LayrCake.WebApi.Controllers.Implementation
{
    public partial class DDDElementController
    {
        // GET tables/DDDElement
        [HttpGet]
        [Route("~/api/dddelement", Name = "GetDDDElementWithMethods")] ///{id:int}
        public IQueryable<DDDElement> GetAllDDDElements([FromUri] int id, [FromUri] bool includeMethods)
        {
            try
            {
                // Before Fetch
                var result = new List<DDDElement>();
                var criteria = new DDDElementVwmCriteria() {
                    Specification = new Specification<DDDElementVwm>(x => x.DDDPackage_Ref == id),
                    DDDElementID = id,
                    IncludeDDDMethodsRecords = includeMethods,
                };
                foreach (var returnItem in _dDDElementRepository.GetListQuery(criteria))
                    result.Add(Mapper.FromBusinessObject(returnItem));
                // After Fetch
                Dispose();
                var list = JsonConvert.SerializeObject(result,
                    Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                var jsonResult = JsonConvert.DeserializeObject<List<DDDElement>>(list);
                Dispose();
                return jsonResult.AsQueryable();
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
    }
}