using PhoneNumbers;
using System.ComponentModel.DataAnnotations;

public class PhoneNumberAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var valueString = value as string;
        if (string.IsNullOrEmpty(valueString))
        {
            return ValidationResult.Success;
        }

        var util = PhoneNumberUtil.GetInstance();
        try
        {
            var number = util.Parse(valueString, "US");
            if (util.IsValidNumber(number))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("The " + valueString + " is not a valid phonenumber");
            }
        }
        catch (NumberParseException)
        {
            return new ValidationResult(valueString + " is not a valid e164 formatted phonenumber");

        }
    }
}