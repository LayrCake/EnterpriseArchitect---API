using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailClient.APIRepositories;
using System.Collections;
using System.Linq;
using Infrastructure.TestsData.HelpersWeb;
using MailClient.Models;

namespace MailClient.Tests
{
    [TestClass]
    public class GoogleMail_Tests : Hosted_BaseActionServiceSetup
    {
        [TestMethod]
        public void GoogleMail_Basic_Test()
        {
            var uri = new Uri("http://localhost:4579");
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest(uri))
            {
                var googleMail = new GoogleMailService();
                Assert.IsNotNull(googleMail);
                var labels = googleMail.GoogleMail_Get_Labels();
                Assert.IsNotNull(labels, "Labels returned null");
                Assert.IsTrue(labels.Count > 0, "Labels returned no results");
            }
        }
    }
}
