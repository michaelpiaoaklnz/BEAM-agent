namespace BeamApi.Models.Requests;

public class LoginAuditRequest
{
    public string UserName { get; set; } = string.Empty;

    public int FailedAttempts { get; set; }

    public string IpAddress { get; set; } = string.Empty;

    public string DeviceFingerprint { get; set; } = string.Empty;
}
