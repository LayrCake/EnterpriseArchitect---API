using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LayrCake.WebApi.Models
{
    public interface IAzureTableData
    {
        string Id { get; set; }
        DateTimeOffset? createdAt { get; set; }
        DateTimeOffset? updatedAt { get; set; }
        bool Deleted { get; set; }
        byte[] Version { get; set; }
    }
}