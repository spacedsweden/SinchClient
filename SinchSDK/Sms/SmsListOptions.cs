using System.Text.Json.Serialization;

namespace Sinch
{
    public class SmsListOptions
    {
        [JsonPropertyName("from")]
        [PhoneNumber]
        public string? From { get; set; }
        
        [JsonPropertyName("to")]
        [PhoneNumber]
        public string? To { get; set; }

        /// <summary>
        /// Only list messages received at or after this date/time. Formatted as ISO-8601: YYYY-MM-DDThh:mm:ss.SSSZ.
        /// </summary>
        [JsonPropertyName("start_date")]
        public DateTime? startTime { get; set; }

        /// <summary>
        /// Only list messages received at or before this date/time. Formatted as ISO-8601: YYYY-MM-DDThh:mm:ss.SSSZ.
        /// </summary>
        [JsonPropertyName("end_date")]
        public DateTime? endTime { get; set; }

        [JsonPropertyName("client_reference")]
        public string ClientReference { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }
    }
}