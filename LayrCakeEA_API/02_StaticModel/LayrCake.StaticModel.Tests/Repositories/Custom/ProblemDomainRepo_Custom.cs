using LayrCake.StaticModel.Criteria.Implementation;
using LayrCake.StaticModel.Repositories;
using LayrCake.StaticModel.Repositories.Abstract;
using LayrCake.StaticModel.Repositories.Implementation;
using LayrCake.StaticModel.ViewModelObjects.Implementation;
using Infrastructure.TestsData.HelpersWeb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vwmo = Infrastructure.TestData._StaticModel.ViewModelObjects;
using NCommon.Specifications;

namespace LayrCake.StaticModel.Tests.Repositories.Custom
{
    [TestClass]
    public class DDDProblemDomainRepo_Custom : BaseTestInitialise
    {
        private IDDDProblemDomainRepository problemDomainRepository;

        [TestInitialize]
        public void Test_Setup()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                problemDomainRepository = new DDDProblemDomainRepository();
            }
        }

        [TestMethod]
        public void Generated_Repository_ProblemDomain_DeleteWhere_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                //var response = problemDomainRepository.Insert(vwmo.VWMbjectsFactory.CreateNew<ProblemDomainVwm>());
                //Assert.IsNotNull(response, "Response object is null");
                //Assert.IsTrue(response.ProblemDomainID > 0, "ResponseProblemDomainId is not greater than 0 - Insert Failed");
                var criteria = new DDDProblemDomainVwmCriteria()
                {
                    Specification = new Specification<DDDProblemDomainVwm>(x => x.DDDProblemDomainID > 4000076)
                };
                var responseDelete = problemDomainRepository.Delete(criteria);
                Assert.IsNull(responseDelete, "Response object is not null");

                //var responseGet = problemDomainRepository.Get(response.ProblemDomainID);
                //Assert.IsNull(responseGet, "Response object was not deleted");
            }
        }
    }
}
