namespace BeamApi.Models.Requests;

public class ProfileAuditRequest
{
    public string UserId { get; set; } = string.Empty;

    public string ActingUserRole { get; set; } = string.Empty;

    public string FieldName { get; set; } = string.Empty;

    public string BeforeValue { get; set; } = string.Empty;

    public string AfterValue { get; set; } = string.Empty;

    public string RequestSource { get; set; } = string.Empty;
}
