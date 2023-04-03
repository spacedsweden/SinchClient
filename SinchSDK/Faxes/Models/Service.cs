using Sinch.FaxApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sinch.FaxApi.Models
{
    public class Service
    {
        /// <summary>
        /// Id of the service returned from sinch
        /// </summary>
        [JsonPropertyName("Id")]
        public Ulid Id { get; set; }

        [JsonPropertyName("incomingFaxCallbackUrl")]
        [Column(TypeName = "varchar(2048)")]
        [MaxLength(2048)]
        public string IncomingFaxCallbackUrl { get; set; } = string.Empty;

        [JsonPropertyName("incomingFaxCallbackUrlFallback")]
        [Column(TypeName = "varchar(2048)")]
        [MaxLength(2048)]
        public string IncomingFaxCallbackUrlFallback { get; set; } = string.Empty;

        [JsonPropertyName("defaultForProject")]
        public bool DefaultForProject { get; set; } = true;

        [JsonPropertyName("enableNameLookup")]
        public bool EnableNameLookup { get; set; } = false;

        [JsonPropertyName("defaultFrom")]
        [PhoneNumber]
        [Column(TypeName = "varchar(20)")]
        [MaxLength(20)]
        public string? DefaultFrom { get; set; }

        [JsonPropertyName("numberOfRetries")]
        public int NumberOfRetries { get; set; } = 5;

        [JsonPropertyName("retryDelaySeconds")]
        public int RetryDelaySeconds { get; set; } = 60;

        [JsonPropertyName("cancelDelayMinutes")]
        public int CancelDelayMinutes { get; set; } = 5;

        [JsonPropertyName("imageConversionMethod")]
        public ImageConversionMethod ImageConversionMethod { get; set; } = ImageConversionMethod.HALFTONE;

        [JsonPropertyName("receiveFaxPartialCompleted")]
        public bool ReceiveFaxPartialCompleted { get; set; } = false;

        [JsonPropertyName("saveSentFaxDocuments")]
        public bool SaveSentFaxDocuments { get; set; } = true;

        [JsonPropertyName("saveReceiveFaxDocuments")]
        public bool SaveReceiveFaxDocuments { get; set; } = true;

        [JsonPropertyName("projectId")]

        public Guid ProjectId { get; set; }
        public virtual IEnumerable<Fax>? Faxes { get; set; }
        public virtual IEnumerable<Number>? Numbers { get; set; }
        [JsonPropertyName("headerTimeZone")]
        [Column(TypeName = "varchar(255)")]
        [MaxLength(255)]
        public string HeaderTimeZone { get; internal set; } = "America/Los_Angeles";
        [JsonPropertyName("headerText")]
        [Column(TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string HeaderText { get; internal set; } = "";
        public bool HeaderPageNumbers { get; internal set; } = true;
    }
}
