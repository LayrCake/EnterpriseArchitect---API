using Newtonsoft.Json.Linq;
using System.IO;
using System.Web;

namespace LayrCake.WebApi.FakeControllers
{
    public abstract class FakeBaseController
    {
        public string _jsonFilePath = "~/bin/FakeData/SampleData.json";
        public string JsonFilePath
        {
            get { return _jsonFilePath; }
            set { _jsonFilePath = value; }
        }

        public JObject Jobject { get; set; }

        public FakeBaseController(string jsonFilePath = null)
        {
            if (!string.IsNullOrEmpty(jsonFilePath))
                JsonFilePath = jsonFilePath;
            
            using (StreamReader r = new StreamReader(HttpContext.Current.Server.MapPath(JsonFilePath)))
            {
                string json = r.ReadToEnd();
                Jobject = JObject.Parse(json);
            }
        }
    }
}