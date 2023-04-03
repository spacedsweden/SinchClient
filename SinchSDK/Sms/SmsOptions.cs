using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sinch.SmsApi.Models
{
    public class SmsOptions
    {
        [JsonRequired]
        [Required]
        
        public string[] To { get; set; }
        [PhoneNumber]
        public string From { get; set; }
        public string Body { get; set; }
        [JsonPropertyName("delivery_report")]
        public string DeliveryReport { get; set; }

        [JsonPropertyName("sent_at")]
        public DateTime SendTime { get; set; }

        [JsonPropertyName("expire_at")]
        public DateTime ExpireTime { get; set; }

        [JsonPropertyName("callback_url")]
        [Url]
        public string CallbackUrl { get; set; }

        [JsonPropertyName("client_reference")]
        public string ClientReference { get; set; }

        [JsonPropertyName("feedback_enabled")]
        public bool FeedbackEnabled { get; set; }

        /// <summary>
        /// If true, long messages will be truncated to 160 characters. If false, long messages will be concatenated.
        /// </summary>
        [JsonPropertyName("truncate_concat")]
        public bool TruncateLongMessages { get; set; }

    }
}
