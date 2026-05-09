using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class ProjectsService
{
    public ApiResponse<object> View(ProjectAccessRequest request)
    {
        var isAdmin = string.Equals(
            request.UserRole,
            "admin",
            StringComparison.OrdinalIgnoreCase);

        if (request.IsConfidentialProject
            && !request.IsProjectMember
            && !isAdmin)
        {
            var denied = ApiResponse<object>.Failure(
                new List<string> { "Access denied for confidential project." },
                "Access denied");
            denied.Data = new
            {
                accessGranted = false,
                projectId = request.ProjectId
            };
            return denied;
        }

        return ApiResponse<object>.Success(
            new
            {
                accessGranted = true,
                projectId = request.ProjectId
            },
            "Project details retrieved");
    }
}
