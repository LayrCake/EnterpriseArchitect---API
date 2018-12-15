using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayrCake.WebApi.Models
{
    public abstract class AzureEntityModel2 : IAzureTableData2 // ITableData // : EntityData 
    {
        //[JsonIgnore]
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? createdAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime? updatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool deleted { get; set; }

        [JsonProperty("version")]
        public byte[] Version { get; set; }
    }
}