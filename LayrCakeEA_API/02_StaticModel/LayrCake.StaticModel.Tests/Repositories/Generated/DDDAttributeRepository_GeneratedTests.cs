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
    public class StaticModel_6_DDDAttributeRepository_Tests : BaseTestInitialise
    {
        private IDDDAttributeRepository dDDAttributeRepository;

        [TestInitialize]
        public void Test_Setup()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                dDDAttributeRepository = new DDDAttributeRepository();
            }
        }
		
        [TestMethod]
        public void Generated_Repository_DDDAttribute_Initialise()
        {
            Assert.IsNotNull(dDDAttributeRepository);
        }

        [TestMethod]
        public void Generated_Repository_DDDAttribute_Insert_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var response = dDDAttributeRepository.Insert(vwmo.VWMbjectsFactory.CreateNew<DDDAttributeVwm>());
                Assert.IsNotNull(response, "Response object is null");
                Assert.IsTrue(response.DDDAttributeID > 0, "Response DDDAttributeId is not greater than 0 - Insert Failed");
            }
        }

        [TestMethod]
        public void Generated_Repository_DDDAttribute_Update_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var response = dDDAttributeRepository.Insert(vwmo.VWMbjectsFactory.CreateNew<DDDAttributeVwm>());
                Assert.IsNotNull(response, "Response object is null");
                Assert.IsTrue(response.DDDAttributeID > 0, "Response DDDAttributeId is not greater than 0 - Insert Failed");

                var responseUpdate = dDDAttributeRepository.Update(response);
                Assert.IsNotNull(responseUpdate, "Response object is null");
                Assert.IsTrue(responseUpdate.DDDAttributeID == response.DDDAttributeID, "Response DDDAttributeId is not greater than 0 - Insert Failed");
            }
        }

        [TestMethod]
        public void Generated_Repository_DDDAttribute_Delete_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var response = dDDAttributeRepository.Insert(vwmo.VWMbjectsFactory.CreateNew<DDDAttributeVwm>());
                Assert.IsNotNull(response, "Response object is null");
                Assert.IsTrue(response.DDDAttributeID > 0, "ResponseDDDAttributeId is not greater than 0 - Insert Failed");

                var responseDelete = dDDAttributeRepository.Delete(response);
                Assert.IsNull(responseDelete, "Response object is not null");

                var responseGet = dDDAttributeRepository.Get(response.DDDAttributeID);
                Assert.IsNull(responseGet, "Response object was not deleted");
            }
        }

        [TestMethod]
        public void Generated_Repository_DDDAttribute_GetAll_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var response = dDDAttributeRepository.GetList();
                Assert.IsNotNull(response, "Response object is null");
                Assert.IsTrue(response.Count > 0, "Response object count is 0");
            }
        }

        [TestMethod]
        public void Generated_Repository_DDDAttribute_GetAll_WithCriteria_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var criteria = new DDDAttributeVwmCriteria();
                var response = dDDAttributeRepository.GetList(criteria);
                Assert.IsNotNull(response, "Response object is null");
                Assert.IsTrue(response.Count > 0, "Response object count is 0");
            }
        }

        [TestMethod]
        public void Generated_Repository_DDDAttribute_GetSingle_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var response = dDDAttributeRepository.Insert(vwmo.VWMbjectsFactory.CreateNew<DDDAttributeVwm>());
                Assert.IsNotNull(response, "Response object is null");
                Assert.IsTrue(response.DDDAttributeID > 0, "Response DDDAttributeId is not greater than 0 - Insert Failed");

                var responseGet = dDDAttributeRepository.Get(response.DDDAttributeID);
                Assert.IsNotNull(responseGet, "Response object is null");
                Assert.IsTrue(responseGet.DDDAttributeID == response.DDDAttributeID, "Response didn't return the correct DDDAttribute record");
            }
        }
	}
}
