using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BeamApi.Attributes;

public class RequiredIfAttribute : ValidationAttribute
{
    private readonly string _dependentProperty;
    private readonly object _targetValue;

    public RequiredIfAttribute(string dependentProperty, object targetValue)
    {
        _dependentProperty = dependentProperty;
        _targetValue = targetValue;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(_dependentProperty);
        if (property == null)
        {
            throw new ArgumentException($"Property {_dependentProperty} does not exist on type {validationContext.ObjectType.Name}");
        }

        var dependentValue = property.GetValue(validationContext.ObjectInstance);
        if (dependentValue == null || !dependentValue.Equals(_targetValue))
        {
            return ValidationResult.Success;
        }

        if (value == null)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}
