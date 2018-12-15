using LayrCake.StaticModel.Criteria.Implementation;
using LayrCake.StaticModel.Repositories;
using LayrCake.StaticModel.Repositories.Abstract;
using LayrCake.StaticModel.Repositories.Implementation;
using LayrCake.StaticModel.ViewModelObjects.Implementation;
using Infrastructure.TestsData.HelpersWeb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vwmo = Infrastructure.TestData._StaticModel.ViewModelObjects;
using NCommon.Specifications;
using System.Linq;
using Infrastructure.Criteria;

namespace LayrCake.StaticModel.Tests.Repositories.Custom
{
    [TestClass]
    public class DDDElementRepo_Custom : BaseTestInitialise
    {
        private IDDDElementRepository _repository;

        [TestInitialize]
        public void Test_Setup()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                _repository = new DDDElementRepository();
            }
        }

        [TestMethod]
        public void Custom_Repository_DDDElement_GetAll_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var criteria = new DDDElementVwmCriteria()
                {
                    //Specification = new Specification<DDDPackageVwm>(x => x.DDDSolutionToPackageLinks.Any(y => (y.DDDSolutionRef == 4000041)))
                };
                var response = _repository.GetList(criteria);
                Assert.IsNotNull(response, "Response object is not null");
                Assert.IsTrue(response.Any(), "Response doesn't contain any records");
            }
        }

        [TestMethod]
        public void Custom_Repository_DDDElement_GetAll_Pagination_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var pageNumber = 1;
                var pageCount = 10;
                var criteria = new DDDElementVwmCriteria()
                {
                    Pagination = new Pagination()
                    {
                        PageNumber = pageNumber,
                        PageSize = pageCount
                    }
                };
                var response = _repository.GetList(criteria);
                Assert.IsNotNull(response, "Response object is not null");
                Assert.IsTrue(response.Any(), "Response doesn't contain any records");
                var pageLastID = response.Last().DDDElementID;

                Assert.IsTrue(response.Count.Equals(pageCount), "Page Size doesn't match:" + response.Count.ToString());

                criteria.Pagination.PageNumber = 2;
                var response2 = _repository.GetList(criteria);
                Assert.IsNotNull(response2, "Response object is not null");
                Assert.IsTrue(response2.Any(), "Response doesn't contain any records");
                var pageLastID2 = response2.Last().DDDElementID;

                Assert.IsTrue(response2.Count.Equals(pageCount), "Page Size doesn't match:" + response2.Count.ToString());
                Assert.IsFalse(pageLastID.Equals(pageLastID2), "Page Size doesn't match:" + response2.Count.ToString());

            }
        }

    }
}
