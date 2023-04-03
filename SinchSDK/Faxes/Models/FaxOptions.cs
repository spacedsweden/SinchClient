using PhoneNumbers;
using Sinch.FaxApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class ValidateFaxContentUrl : ValidationAttribute
{
    private bool IsUrl(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        return value is string valueAsString &&
            (valueAsString.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
            || valueAsString.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
            || valueAsString.StartsWith("ftp://", StringComparison.OrdinalIgnoreCase));
    }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        bool contentUrlIsValid = false;
        if (validationContext.ObjectType.GetProperty("ContentUrl") == null || validationContext.ObjectType.GetProperty("ContentUrl").GetValue(validationContext.ObjectInstance, null) == null)
            contentUrlIsValid = false;
        else
        {
            if (validationContext.ObjectType.GetProperty("ContentUrl").GetValue(validationContext.ObjectInstance, null).GetType() == typeof(string))
            {
                if (IsUrl((string)validationContext.ObjectType.GetProperty("ContentUrl").GetValue(validationContext.ObjectInstance, null)))
                {
                    contentUrlIsValid = true;

                }
                else
                {
                    contentUrlIsValid = false;
                    return new ValidationResult((string)validationContext.ObjectType.GetProperty("ContentUrl").GetValue(validationContext.ObjectInstance, null) + " is not a valid URL");
                }
            }
            else if (validationContext.ObjectType.GetProperty("ContentUrl").GetValue(validationContext.ObjectInstance, null).GetType() == typeof(string[]))
            {
                var urls = (string[])validationContext.ObjectType.GetProperty("ContentUrl").GetValue(validationContext.ObjectInstance, null);
                foreach (var url in urls)
                {
                    if (!IsUrl(url))
                    {
                        contentUrlIsValid = false;
                        return new ValidationResult(url + " is not a valid URL");
                    }
                    contentUrlIsValid = true;
                }
            }
        }

        //var file = (IFormFile[])validationContext.ObjectType.GetProperty("File").GetValue(validationContext.ObjectInstance, null);
        //check at least one has a value
        return ValidationResult.Success;
    }
}




public class FaxOptions
{
    
    [JsonPropertyName("from")]
    [PhoneNumber]
    public string From { get; set; } = string.Empty;

    /// <summary>
    /// The phone number to send the fax to. This can be a single number or a comma-separated list of numbers.
    /// </summary>
    /// <example>+12402249564</example>
    [JsonPropertyName("to")]
    [Required]
    [PhoneNumber]

    public string? To { get; set; }

    /// <summary>
    /// The URL of the file to send. This can be a PDF, PNG, JPG, or TIFF file. The file must be publicly accessible.
    /// </summary>
    /// <example>https://google.com</example>
    [JsonPropertyName("contentUrl")]
    //[ValidateFaxContentUrl]
    //[Url]
    public Uri[]? ContentUrl { get; set; }

    [JsonPropertyName("headerText")]
    public string? HeaderText { get; set; }

    [JsonPropertyName("headerPageNumbers")]
    public bool? HeaderPageNumbers { get; set; } = true;

    [JsonPropertyName("headerTimeZone")]
    public string? HeaderTimeZone { get; set; }// = "America/Los_Angeles";

    [JsonPropertyName("cancelTimeout")]
    public int? CancelTimeout { get; set; }// = 30;

    [JsonPropertyName("labels")]
    public Dictionary<string, string>? Labels { get; set; }

    [JsonPropertyName("callbackUrl")]
    [Url]
    public string? CallbackUrl { get; set; }

    [JsonPropertyName("imageConversionMethod")]
    public ImageConversionMethod? ImageConversionMethod { get; set; } //= ImageConversionMethod.HALFTONE;

    [JsonPropertyName("serviceId")]
    public Ulid? ServiceId { get; set; }

}