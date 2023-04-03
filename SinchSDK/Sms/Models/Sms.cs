// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System.Text.Json.Serialization;
namespace Sinch.SmsApi.Models
{
    public class ParameterKey
    {
        [JsonPropertyName("{msisdn}")]
        public string Msisdn { get; set; }

        [JsonPropertyName("default")]
        public string Default { get; set; }
    }

    public class Parameters
    {
        [JsonPropertyName("{parameter_key}")]
        public ParameterKey ParameterKey { get; set; }
    }

    public class Sms
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("to")]
        public List<object> To { get; set; }

        [JsonPropertyName("from")]
        public long? From { get; set; }

        [JsonPropertyName("canceled")]
        public bool? Canceled { get; set; }

        [JsonPropertyName("parameters")]
        public Parameters Parameters { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreateTime { get; set; }

        [JsonPropertyName("modified_at")]
        public DateTime? ModifiedTime { get; set; }

        [JsonPropertyName("delivery_report")]
        public string DeliveryReport { get; set; }

        [JsonPropertyName("send_at")]
        public DateTime? SendAt { get; set; }

        [JsonPropertyName("expire_at")]
        public DateTime? ExpireTime { get; set; }

        [JsonPropertyName("callback_url")]
        public string CallbackUrl { get; set; }

        [JsonPropertyName("client_reference")]
        public string ClientReference { get; set; }

        [JsonPropertyName("feedback_enabled")]
        public bool? FeedbackEnabled { get; set; }

        [JsonPropertyName("flash_message")]
        public bool? FlashMessage { get; set; }

        [JsonPropertyName("truncate_concat")]
        public bool? TruncateLongMessages { get; set; }

        [JsonPropertyName("max_number_of_message_parts")]
        public int? MaxNumberOfMessageParts { get; set; }

        [JsonPropertyName("from_ton")]
        public int? FromTon { get; set; }

        [JsonPropertyName("from_npi")]
        public int? FromNpi { get; set; }
    }

}