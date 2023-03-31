// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Sinch.FaxApi.Models
{


    public class BarCode
    {
        public string Id { get; set; } 
        [JsonPropertyName("type")]
        public string Type { get; set; } = "CODE_128";

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("value")]

        public string? Value { get; set; }
    }


    public class Money
    {
        [JsonPropertyName("currencyCode")]
        public string? CurrencyCode { get; set; }

        [JsonPropertyName("amount")]
        public string? Amount { get; set; }
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Direction { SENT, RECEIVED };

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ImageConversionMethod
    {
        HALFTONE, MONOCHROME
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        QUEUED, PENDING_BATCH, IN_PROGRESS, COMPLETED, FAILED, CANCELLED, NO_ANSWER, BUSY
    }


    [JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
    public class Fax
    {
        [JsonPropertyName("id")]
        [JsonPropertyOrder(1)]
        public Ulid Id { get; set; }
        [JsonIgnore]
        public long PhaxioId { get; set; }
        [JsonPropertyOrder(4)]
        [JsonPropertyName("direction")]
        [MaxLength(12)]
        [Column(TypeName = "varchar(12)")]
        public Direction Direction { get; set; } = Direction.SENT;
        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        [JsonPropertyName("from")]
        [JsonPropertyOrder(2)]
        public string From { get; set; } = string.Empty;
        [JsonPropertyOrder(3)]
        [JsonPropertyName("to")]
        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string To { get; set; } = string.Empty;

        [JsonPropertyName("contentUrl")]
        [JsonPropertyOrder(5)]
        public string[]? ContentUrl { get; set; }
        [JsonIgnore]
        public string[]? DebugFileIds { get; set; }

        [JsonPropertyOrder(7)]
        [JsonPropertyName("numberOfPages")]
        public int? NumberOfPages { get; set; }

        [JsonPropertyOrder(6)]
        [JsonPropertyName("status")]
        public Status Status { get; set; } = Status.QUEUED;
        [JsonPropertyOrder(8)]
        [JsonPropertyName("price")]
        public Money? Price { get; set; }
        [JsonPropertyOrder(60)]
        [JsonPropertyName("barCodes")]
        public List<BarCode>? BarCodes { get; set; }
        [JsonPropertyOrder(50)]
        [JsonPropertyName("createTime")]

        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("answerTime")]
        public DateTime? AnswerTime { get; set; }

        [JsonPropertyName("endTime")]
        public DateTime? EndTime { get; set; }
        [JsonPropertyOrder(50)]
        [JsonPropertyName("completedTime")]
        public DateTime? CompletedTime { get; set; }

        [JsonPropertyName("durationSeconds")]
        public int? DurationSeconds { get; set; }
        [JsonPropertyOrder(10)]
        [JsonPropertyName("headerText")]
        [Column(TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string HeaderText { get; set; } = string.Empty;
        [JsonPropertyOrder(11)]
        [JsonPropertyName("headerPageNumbers")]
        public bool HeaderPageNumbers { get; set; } = true;

        [JsonPropertyOrder(12)]
        [JsonPropertyName("headerTimeZone")]
        [Column(TypeName = "varchar(100)")]
        [MaxLength(100)]
        public string? HeaderTimeZone { get; set; } = "Americas/New York";
        [JsonPropertyOrder(100)]
        [JsonPropertyName("cancelTimeout")]
        public int CancelTimeout { get; set; } = 30;
        [JsonPropertyOrder(80)]
        [JsonPropertyName("labels")]
        public Dictionary<string, string>? Labels { get; set; }
        [JsonPropertyOrder(13)]
        [JsonPropertyName("callbackUrl")]
        [MaxLength(2048)]
        [Column(TypeName = "varchar(2048)")]
        public string CallbackUrl { get; set; } = string.Empty;
        [JsonPropertyOrder(14)]
        [JsonPropertyName("imageConversionMethod")]
        [MaxLength(30)]
        [Column(TypeName = "varchar(30)")]
        public ImageConversionMethod ImageConversionMethod { get; set; } = ImageConversionMethod.HALFTONE;
        [JsonPropertyOrder(15)]
        [JsonPropertyName("errorType")]
        [MaxLength(1000)]
        [Column(TypeName = "varchar(1000)")]
        public string ErrorType { get; set; } = string.Empty;
        [JsonPropertyOrder(16)]
        [JsonPropertyName("errorId")]
        public int ErrorId { get; set; } = 0;
        [JsonPropertyOrder(17)]
        [JsonPropertyName("errorCode")]
        [MaxLength(1000)]
        [Column(TypeName = "varchar(1000)")]
        public string ErrorCode { get; set; } = string.Empty;
        [JsonPropertyOrder(18)]
        [JsonPropertyName("projectId")]


        public Guid ProjectId { get; set; }
        [JsonPropertyOrder(19)]
        [JsonPropertyName("serviceId")]

        public Ulid ServiceId { get; set; }
        [JsonPropertyOrder(80)]
        [JsonPropertyName("maxRetries")]
        public int MaxRetries { get; set; } = 5;
        [JsonPropertyOrder(80)]
        [JsonPropertyName("retryCount")]
        public int? RetryCount { get; set; }
        [JsonPropertyOrder(70)]
        [JsonPropertyName("isTest")]
        public bool? IsTest { get; set; }
        [JsonPropertyOrder(80)]
        [JsonPropertyName("batchDelay")]

        public int? BatchDelay { get; set; }
        [JsonPropertyOrder(80)]
        [JsonPropertyName("batchCollisionAvoidance")]
        public bool? BatchCollisionAvoidance { get; set; }

        [JsonIgnore]
        public bool isRated { get; set; }

    }
    public class Detail
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("fieldViolations")]
        public List<FieldViolation>? FieldViolations { get; set; }
    }

    public class FieldViolation
    {
        [JsonPropertyName("field")]
        public string? Field { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}