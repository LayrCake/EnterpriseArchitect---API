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

namespace LayrCake.StaticModel.Tests.Repositories.Custom
{
    [TestClass]
    public class DDDPackageRepo_Custom : BaseTestInitialise
    {
        private IDDDPackageRepository DDDPackageRepository;

        [TestInitialize]
        public void Test_Setup()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                DDDPackageRepository = new DDDPackageRepository();
            }
        }

        [TestMethod]
        public void Generated_Repository_DDDPackage_DeleteWhere_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                //var response = DDDPackageRepository.Insert(vwmo.VWMbjectsFactory.CreateNew<DDDPackageVwm>());
                //Assert.IsNotNull(response, "Response object is null");
                //Assert.IsTrue(response.DDDPackageID > 0, "ResponseDDDPackageId is not greater than 0 - Insert Failed");
                var criteria = new DDDPackageVwmCriteria()
                {
                    //Specification = new Specification<DDDPackageVwm>(x => x.DDDSolutionToPackageLinks.Any(y => (y.DDDSolutionRef == 4000041)))
                };
                var responseDelete = DDDPackageRepository.GetList(criteria);
                Assert.IsNull(responseDelete, "Response object is not null");

                //var responseGet = DDDPackageRepository.Get(response.DDDPackageID);
                //Assert.IsNull(responseGet, "Response object was not deleted");
            }
        }
    }
}
