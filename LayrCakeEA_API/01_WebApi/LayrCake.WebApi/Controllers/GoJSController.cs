using LayrCake.StaticModel.Criteria.Implementation;
using LayrCake.StaticModel.Repositories.Abstract;
using LayrCake.StaticModel.Repositories.Implementation;
using LayrCake.StaticModel.ViewModelObjects.Implementation;
using LayrCake.WebApi.ModelMapper;
using LayrCake.WebApi.Models;
using LayrCake.WebApi.Properties;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using NCommon.Specifications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Tracing;

namespace LayrCake.WebApi.Controllers
{
    [MobileAppController]
    public partial class GoJSController : ApiController //BaseController
    {
        private IDDDElementRepository _repositoryElement;
        private IDDDMethodRepository _repositoryMethod;
        private IDDDConnectorRepository _repositoryConnector;
        internal ITraceWriter traceWriter;

        public GoJSController() : this(new DDDElementRepository(), new DDDMethodRepository(), new DDDConnectorRepository()) { }

        public GoJSController(IDDDElementRepository repositoryElement, IDDDMethodRepository repositoryMethod, IDDDConnectorRepository repositoryConnector)
		{
            _repositoryElement = repositoryElement ?? new DDDElementRepository();
            _repositoryMethod = repositoryMethod ?? new DDDMethodRepository();
            _repositoryConnector = repositoryConnector ?? new DDDConnectorRepository();
        }

        [HttpGet]
        [Route("tables/dbdiagramdata", Name = "GetDbDiagramDataId")]
        public virtual JsonResult<string> DbDiagramData(int? packageRef = null)
        {
            var specPackageRef = new Specification<DDDElementVwm>(i => i.DDDPackage_Ref == packageRef);

            var criteria = new DDDElementVwmCriteria
            {
                IncludeDDDMethodsRecords = true,
                Specification = specPackageRef
            };
            var modelList = _repositoryElement.GetList(criteria);
            var list = JsonConvert.SerializeObject(modelList,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            var jsonResult = Json(list, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MaxDepth = int.MaxValue
            });
            return jsonResult;
        }

        [HttpGet]
        [Route("tables/dbdiagramdata_gojs", Name = "GetDbDiagramData_GoJsId")]
        public virtual JsonResult<string> DbDiagramData_GoJs(int? packageRef = null)
        {
            var specPackageRef = new Specification<DDDElementVwm>(i => i.DDDPackage_Ref == packageRef);

            var criteria = new DDDElementVwmCriteria
            {
                IncludeDDDMethodsRecords = true,
                Specification = specPackageRef
            };
            var modelList = _repositoryElement.GetList(criteria); //.GetRange(0, 4)

            var goList = AutoMapper.Mapper.Map<List<GoJsElement>>(modelList);
            var list = JsonConvert.SerializeObject(goList,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            var jsonResult = Json(list, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MaxDepth = int.MaxValue
            });
            return jsonResult;
        }

        [HttpGet]
        [Route("tables/dbdiagramdatacombined_gojs", Name = "GetDbDiagramDataCombined_GoJsId")]
        public virtual JsonResult<GoJsElementLinks> DbDiagramDataCombined_GoJs(int packageRef)
        {
            var criteria = new DDDElementVwmCriteria
            {
                IncludeDDDMethodsRecords = true,
                Specification = new Specification<DDDElementVwm>(i => i.DDDPackage_Ref == packageRef)
            };
            var modelList = _repositoryElement.GetList(criteria); //.GetRange(0, 4)

            var goList = AutoMapper.Mapper.Map<List<GoJsElement>>(modelList);

            var criteriaConnector = new DDDConnectorVwmCriteria {Specification = new Specification<DDDConnectorVwm>(i => i.DDDPackage_Ref == packageRef && i.IsArray == 1) };
            var modelListConnector = _repositoryConnector.GetList(criteriaConnector); //.GetRange(0, 4)

            var goListConnector = AutoMapper.Mapper.Map<List<GoJsConnector>>(modelListConnector);

            var list = JsonConvert.SerializeObject(goList,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    MaxDepth = int.MaxValue
                });
            var list2 = JsonConvert.SerializeObject(goListConnector,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    MaxDepth = int.MaxValue
                });

            var jsonResult = Json(new GoJsElementLinks(){ key = packageRef, nodes = list, links = list2 });
            return jsonResult;
        }

        [HttpPost]
        [Route("tables/dbdiagramdatacombined_gojs")]
        public async Task<IHttpActionResult> Post_DbDiagramDataCombined_GoJs([FromBody]GoJsElementLink_Post item)
        {
            if (item == null || item.nodeDataArray == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, TResources.TableController_NullRequestBody));

            if (!ModelState.IsValid)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));

            try
            {
                var goNodes = AutoMapper.Mapper.Map<List<DDDElementVwm>>(item.nodeDataArray);
                var goLinks = AutoMapper.Mapper.Map<List<DDDConnectorVwm>>(item.linkDataArray);

                for (var i = 0; i < goNodes.Count; i++)
                {
                    if (goNodes[i].DDDElementID < 1)
                    {
                        goNodes[i].DDDPackage_Ref = item.packageRef;
                        _repositoryElement.Insert(goNodes[i]);
                    }
                    else if (goNodes[i].DDDMethods.Any(x => x.DDDMethodID < 1 || x.Updated.HasValue) || goNodes[i].Updated.HasValue)
                    {
                        goNodes[i].DDDMethods.ForEach(x => x.DDDElement_Ref = goNodes[i].DDDElementID);
                        _repositoryElement.Update(goNodes[i]);
                    }
                }
                return CreatedAtRoute("GetDbDiagramDataCombined_GoJsId", new { id = item.packageRef }, item);
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

        [HttpGet]
        [Route("tables/dbdiagramconnector_gojs", Name = "GetDbDiagramConnector_GoJsId")]
        public virtual JsonResult<string> DbDiagramConnector_GoJs(int? packageRef = null)
        {
            var specPackageRef = new Specification<DDDConnectorVwm>(i => i.DDDPackage_Ref == packageRef && i.IsArray == 1);

            var criteria = new DDDConnectorVwmCriteria
            {
                Specification = specPackageRef
            };
            var modelList = _repositoryConnector.GetList(criteria); //.GetRange(0, 4)

            var goList = AutoMapper.Mapper.Map<List<GoJsConnector>>(modelList);
            var list = JsonConvert.SerializeObject(goList,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            var jsonResult = Json(list, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MaxDepth = int.MaxValue,
                
            });
            return jsonResult;
        }

        [HttpGet]
        [Route("tables/dbdiagramdata_enum", Name = "GetDbDiagramData_EnumId")]
        public IEnumerable<DDDElementVwm> DbDiagramData_Enum(int? packageRef = null)
        {
            var specPackageRef = new Specification<DDDElementVwm>(i => i.DDDPackage_Ref == packageRef);

            var criteria = new DDDElementVwmCriteria
            {
                IncludeDDDMethodsRecords = true,
                Specification = specPackageRef
            };
            var modelList = _repositoryElement.GetList(criteria);

            return modelList;
        }
    }
}