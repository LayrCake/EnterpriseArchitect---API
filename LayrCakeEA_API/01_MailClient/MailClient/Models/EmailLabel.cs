using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Models
{
    public class EmailLabel
    {
        public string ETag { get; set; }

        public string Id { get; set; }

        public string MessageListVisibility { get; set; }

        public int? MessagesTotal { get; set; }

        public int? MessagesUnread { get; set; }

        public string Name { get; set; }

        public int? ThreadsTotal { get; set; }

        public int? ThreadsUnread { get; set; }

        public string Type { get; set; }
    }
}
