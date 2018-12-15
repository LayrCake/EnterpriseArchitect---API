using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LayrCake.WebApi.Models
{
    public interface IAzureTableData2
    {
        DateTime? createdAt { get; set; }

        bool deleted { get; set; }

        string Id { get; set; }

        DateTime? updatedAt { get; set; }

        byte[] Version { get; set; }
    }
}