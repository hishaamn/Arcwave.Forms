using System.Collections.Generic;

namespace Arcwave.Forms.Models
{
    public class FormData
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }

    public class EmailData
    {
        public string ContactIdentifier { get; set; }

        public List<FormData> FormData { get; set; }

        public string EmailCampaignId { get; set; }

        public string Source { get; set; }
    }
}