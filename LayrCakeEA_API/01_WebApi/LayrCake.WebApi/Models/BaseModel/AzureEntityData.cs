using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayrCake.WebApi.Models
{
    public abstract class AzureEntityModel : IAzureTableData // ITableData // : EntityData 
    {
        //[JsonIgnore]
        //[JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTimeOffset? createdAt { get; set; }

        //[JsonProperty("createdAt")]
        public int CreatedBy { get; set; }

        [JsonProperty("updatedAt")]
        public DateTimeOffset? updatedAt { get; set; }

        //[JsonProperty("updatedAt")]
        public int? UpdatedBy { get; set; }

        //[JsonProperty("deleted")]
        public bool Deleted { get; set; }
        public int? DeletedBy { get; set; }

        [JsonProperty("version")]
        public byte[] Version { get; set; }
    }
}