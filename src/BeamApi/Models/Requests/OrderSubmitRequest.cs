using System.ComponentModel.DataAnnotations;

namespace BeamApi.Models.Requests;

public class OrderSubmitRequest
{
    [Required]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    public string ShippingAddress { get; set; } = string.Empty;

    public bool ContainsFragileItems { get; set; }

    [ConditionalRequired(nameof(ContainsFragileItems), true)]
    public string? DeliveryInstructions { get; set; }
}

public class ConditionalRequiredAttribute : ValidationAttribute
{
    private readonly string _dependentProperty;
    private readonly object _expectedValue;

    public ConditionalRequiredAttribute(string dependentProperty, object expectedValue)
    {
        _dependentProperty = dependentProperty;
        _expectedValue = expectedValue;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(_dependentProperty);
        if (property == null)
        {
            throw new ArgumentException($"Property {_dependentProperty} not found.");
        }

        var dependentValue = property.GetValue(validationContext.ObjectInstance);
        if (dependentValue != null && dependentValue.Equals(_expectedValue))
        {
            if (value == null)
            {
                return new ValidationResult($"{validationContext.DisplayName} is required.");
            }
        }

        return ValidationResult.Success;
    }
}
