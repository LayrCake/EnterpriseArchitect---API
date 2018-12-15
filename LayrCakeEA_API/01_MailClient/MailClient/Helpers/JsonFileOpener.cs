using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Helpers
{
    public static class JsonFileOpener
    {
        public static string OpenFile(string fileName)
        {
            var allText = File.ReadAllText(fileName);
            object jsonObject = JsonConvert.DeserializeObject(allText);
            return allText;
        }
    }
}
