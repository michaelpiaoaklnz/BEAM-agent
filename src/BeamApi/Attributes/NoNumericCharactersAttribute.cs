using System;
using System.ComponentModel.DataAnnotations;

namespace BeamApi.Attributes;

public class NoNumericCharactersAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        string middleName = value.ToString();
        if (System.Text.RegularExpressions.Regex.IsMatch(middleName, @"\d"))
        {
            return new ValidationResult("Middle name cannot contain numeric characters.");
        }

        return ValidationResult.Success;
    }
}
