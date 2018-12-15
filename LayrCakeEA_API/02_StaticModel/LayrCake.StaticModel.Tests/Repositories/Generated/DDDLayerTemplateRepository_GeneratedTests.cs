﻿/*------------------------------------------------------------------------------
<auto-generated>
     This code was generated by a tool.
	    Code originates from EA Uml ClassTemplate.t4
     Changes to this file will be lost if the code is regenerated.
	    Code Generated Date: 	10 June 2018
	    ProjectModel: 			LayrCake
	    Requested Namespace:	Model$2. Hosting Model$LayrCake.StaticModel$LayrCake$StaticModel$ViewModelObjects$Implementation
</auto-generated>
------------------------------------------------------------------------------*/
using LayrCake.StaticModel.Criteria.Implementation;
using LayrCake.StaticModel.Repositories;
using LayrCake.StaticModel.Repositories.Abstract;
using LayrCake.StaticModel.Repositories.Implementation;
using LayrCake.StaticModel.ViewModelObjects.Implementation;
using Infrastructure.TestsData.HelpersWeb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vwmo = Infrastructure.TestData._StaticModel.ViewModelObjects;

namespace LayrCake.StaticModel.Tests.Repositories.Generated.LiveModelTests
{
    [TestClass]
    public class StaticModel_6_DDDLayerTemplateRepository_Tests : BaseTestInitialise
    {
        private IDDDLayerTemplateRepository dDDLayerTemplateRepository;

        [TestInitialize]
        public void Test_Setup()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                dDDLayerTemplateRepository = new DDDLayerTemplateRepository();
            }
        }
		
        [TestMethod]
        public void Generated_Repository_DDDLayerTemplate_Initialise()
        {
            Assert.IsNotNull(dDDLayerTemplateRepository);
        }

        [TestMethod]
        public void Generated_Repository_DDDLayerTemplate_Insert_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var response = dDDLayerTemplateRepository.Insert(vwmo.VWMbjectsFactory.CreateNew<DDDLayerTemplateVwm>());
                Assert.IsNotNull(response, "Response object is null");
                Assert.IsTrue(response.DDDLayerTemplateID > 0, "Response DDDLayerTemplateId is not greater than 0 - Insert Failed");
            }
        }

        [TestMethod]
        public void Generated_Repository_DDDLayerTemplate_Update_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var response = dDDLayerTemplateRepository.Insert(vwmo.VWMbjectsFactory.CreateNew<DDDLayerTemplateVwm>());
                Assert.IsNotNull(response, "Response object is null");
                Assert.IsTrue(response.DDDLayerTemplateID > 0, "Response DDDLayerTemplateId is not greater than 0 - Insert Failed");

                var responseUpdate = dDDLayerTemplateRepository.Update(response);
                Assert.IsNotNull(responseUpdate, "Response object is null");
                Assert.IsTrue(responseUpdate.DDDLayerTemplateID == response.DDDLayerTemplateID, "Response DDDLayerTemplateId is not greater than 0 - Insert Failed");
            }
        }

        [TestMethod]
        public void Generated_Repository_DDDLayerTemplate_Delete_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var response = dDDLayerTemplateRepository.Insert(vwmo.VWMbjectsFactory.CreateNew<DDDLayerTemplateVwm>());
                Assert.IsNotNull(response, "Response object is null");
                Assert.IsTrue(response.DDDLayerTemplateID > 0, "ResponseDDDLayerTemplateId is not greater than 0 - Insert Failed");

                var responseDelete = dDDLayerTemplateRepository.Delete(response);
                Assert.IsNull(responseDelete, "Response object is not null");

                var responseGet = dDDLayerTemplateRepository.Get(response.DDDLayerTemplateID);
                Assert.IsNull(responseGet, "Response object was not deleted");
            }
        }

        [TestMethod]
        public void Generated_Repository_DDDLayerTemplate_GetAll_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var response = dDDLayerTemplateRepository.GetList();
                Assert.IsNotNull(response, "Response object is null");
                Assert.IsTrue(response.Count > 0, "Response object count is 0");
            }
        }

        [TestMethod]
        public void Generated_Repository_DDDLayerTemplate_GetAll_WithCriteria_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var criteria = new DDDLayerTemplateVwmCriteria();
                var response = dDDLayerTemplateRepository.GetList(criteria);
                Assert.IsNotNull(response, "Response object is null");
                Assert.IsTrue(response.Count > 0, "Response object count is 0");
            }
        }

        [TestMethod]
        public void Generated_Repository_DDDLayerTemplate_GetSingle_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var response = dDDLayerTemplateRepository.Insert(vwmo.VWMbjectsFactory.CreateNew<DDDLayerTemplateVwm>());
                Assert.IsNotNull(response, "Response object is null");
                Assert.IsTrue(response.DDDLayerTemplateID > 0, "Response DDDLayerTemplateId is not greater than 0 - Insert Failed");

                var responseGet = dDDLayerTemplateRepository.Get(response.DDDLayerTemplateID);
                Assert.IsNotNull(responseGet, "Response object is null");
                Assert.IsTrue(responseGet.DDDLayerTemplateID == response.DDDLayerTemplateID, "Response didn't return the correct DDDLayerTemplate record");
            }
        }
	}
}
