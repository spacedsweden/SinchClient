// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using Sinch.FaxApi.Models;
using System.Text.Json.Serialization;
namespace Sinch.Models
{
    public class Error
    {
        [JsonPropertyName("code")]
        public int? code { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("details")]
        public List<Detail>? Details { get; set; }
    }
}