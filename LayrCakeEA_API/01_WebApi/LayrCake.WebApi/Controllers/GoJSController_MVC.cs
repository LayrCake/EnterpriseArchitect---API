using LayrCake.StaticModel.Criteria.Implementation;
using LayrCake.StaticModel.DataVisualiserServiceReference;
using LayrCake.StaticModel.Repositories.Abstract;
using LayrCake.StaticModel.Repositories.Implementation;
using LayrCake.StaticModel.ViewModelObjects.Implementation;
using NCommon.Specifications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Linq;
using LayrCake.WebApi.Models;
using LayrCake.WebApi.Controllers.BaseControllers;

namespace LayrCake.WebApi.Controllers
{
    public partial class GoJSController_MVC : BaseController
    {
        private IDDDElementRepository _repositoryElement;
        private IDDDConnectorRepository _repositoryConnector;

        public GoJSController_MVC() : this(new DDDElementRepository(), new DDDConnectorRepository()) { }

        public GoJSController_MVC(IDDDElementRepository repositoryElement, IDDDConnectorRepository repositoryConnector)
		{
            _repositoryElement = repositoryElement ?? new DDDElementRepository();
            _repositoryConnector = repositoryConnector ?? new DDDConnectorRepository();
        }

        public virtual JsonResult DbDiagramData(int? packageRef = null)
        {
            var specPackageRef = new Specification<DDDElementVwm>(i => i.DDDPackage_Ref == packageRef);

            var criteria = new DDDElementVwmCriteria
            {
                IncludeDDDMethodsRecords = true,
                Specification = specPackageRef
            };
            var modelList = _repositoryElement.GetList(criteria); //.GetRange(0, 4)

            var list = JsonConvert.SerializeObject(modelList,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public virtual JsonResult DbDiagramData_GoJs(int? packageRef = null)
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

            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public virtual JsonResult DbDiagramDataCombined_GoJs(int? packageRef = null)
        {
            var criteria = new DDDElementVwmCriteria
            {
                IncludeDDDMethodsRecords = true,
                Specification = new Specification<DDDElementVwm>(i => i.DDDPackage_Ref == packageRef)
            };
            var modelList = _repositoryElement.GetList(criteria); //.GetRange(0, 4)

            var goList = AutoMapper.Mapper.Map<List<GoJsElement>>(modelList);

            var criteriaConnector = new DDDConnectorVwmCriteria {Specification = new Specification<DDDConnectorVwm>(i => i.DDDPackage_Ref == packageRef) };
            var modelListConnector = _repositoryConnector.GetList(criteriaConnector).Where(x => x.IsArray == 1); //.GetRange(0, 4)

            var goListConnector = AutoMapper.Mapper.Map<List<GoJsConnector>>(modelListConnector);

            var list = JsonConvert.SerializeObject(goList,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            var list2 = JsonConvert.SerializeObject(goListConnector,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            var jsonResult = Json(new { nodes = list, links = list2 }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public virtual JsonResult DbDiagramConnector_GoJs(int? packageRef = null)
        {
            var specPackageRef = new Specification<DDDConnectorVwm>(i => i.DDDPackage_Ref == packageRef);

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

            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


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