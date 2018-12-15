using System.Collections.Generic;
using System.Web.Http;
using LayrCake.StaticModel.Criteria.Implementation;
using LayrCake.StaticModel.Repositories.Abstract;
using LayrCake.StaticModel.Repositories.Implementation;
using LayrCake.StaticModel.ViewModelObjects.Implementation;
using NCommon.Specifications;
using Serialize.Linq.Serializers;

namespace LayrCake.WebApi.Controllers
{
    //[RoutePrefix("api/GoJsApi")]
    public class GoJsApiController : ApiController
    {
        private IDDDElementRepository _repository;

		public GoJsApiController() : this(new DDDElementRepository()){}

        public GoJsApiController(IDDDElementRepository repository)
		{
			_repository = repository ?? new DDDElementRepository();
		}

        // GET: api/GoJsApi
        //[Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/GoJsApi/5
        [Route("{id:int}")]
        public IEnumerable<DDDElementVwm> Get(int id)
        {
            _repository = _repository ?? new DDDElementRepository();

            var specPackageRef = new Specification<DDDElementVwm>(i => i.DDDPackageRef == id);

            var criteria = new DDDElementVwmCriteria
            {
                //IncludeDDDMethodsRecords = true,
                Specification = specPackageRef
            };
            var modelList = _repository.GetList(criteria);
            var serializer = new ExpressionSerializer(new XmlSerializer());
            //var returnModel = serializer.SerializeText(modelList)
            return modelList;
        }

        // POST: api/GoJsApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GoJsApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GoJsApi/5
        public void Delete(int id)
        {
        }
    }
}
