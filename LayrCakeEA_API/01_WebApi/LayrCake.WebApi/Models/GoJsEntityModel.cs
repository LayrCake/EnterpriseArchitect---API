using System.Collections.Generic;

namespace LayrCake.WebApi.Models
{
    public class GoJsElementLinks : AzureEntityModel
    {
        public int key { get; set; }
        public string nodes { get; set; }
        public string links { get; set; }
    }

    public class GoJsElementLink_Post : AzureEntityModel
    {
        public int packageRef { get; set; }
        public List<GoJsElement> nodeDataArray { get; set; }
        public List<GoJsConnector> linkDataArray { get; set; }
    }

    public class GoJsElement : AzureEntityModel
    {
        public int key { get; set; }
        public string name { get; set; }
        public List<GoJsMethod> methods { get; set; }
    }

    public class GoJsMethod : AzureEntityModel
    {
        public int key { get; set; }
        public string name { get; set; }
        public string returnType { get; set; }
        public GoJsElement element { get; set; }
    }

    public class GoJsConnector : AzureEntityModel
    {
        public int key { get; set; }
        public int from { get; set; }
        public int to { get; set; }
    }
}
