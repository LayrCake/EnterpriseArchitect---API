using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Models
{
    public class EmailMessage
    {
        public string ETag { get; set; }

        public string Id { get; set; }

        public ulong? HistoryId { get; set; }

        public long? InternalDate { get; set; }

        public List<string> LabelIds { get; set; }

        public List<EmailMessagePart> Payload { get; set; }

        public string Raw { get; set; }

        public int? SizeEstimate { get; set; }

        public string Snippet { get; set; }

        public string ThreadId { get; set; }
    }
}
