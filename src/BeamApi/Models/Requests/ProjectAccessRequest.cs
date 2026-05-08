namespace BeamApi.Models.Requests;

public class ProjectAccessRequest
{
    public string UserRole { get; set; } = string.Empty;

    public bool IsProjectMember { get; set; }

    public bool IsConfidentialProject { get; set; }

    public string ProjectId { get; set; } = string.Empty;
}