using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net.Http;

namespace LayrCake.WebApi.Tests
{
    [TestClass]
    public abstract class BaseTestInitialise
    {
        public static string ServiceBaseURL = "http://192.168.0.142:2344/";
        public HttpClient testClient;

        [TestInitialize]
        public virtual void TestInitialise()
        {
            testClient = new HttpClient()
            {
                BaseAddress = new Uri(ServiceBaseURL)
            };
        }
    }

    public interface IRequest
    {
        IResponse GetResponse(string url);
        IResponse GetResponse(string url, object data);
    }

    public interface IResponse
    {
        Stream GetStream();
    }
}
