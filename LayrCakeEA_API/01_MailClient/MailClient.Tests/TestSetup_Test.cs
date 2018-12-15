using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace MailClient.Tests
{
    [TestClass]
    public class TestSetup_Test : Hosted_BaseActionServiceSetup
    {
        [TestMethod]
        public void WhenRequestingDefaultPageThenGetResponseWithCode200()
        {
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:12345/default.aspx");

            var response = (HttpWebResponse)request.GetResponse();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
