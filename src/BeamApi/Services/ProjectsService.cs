using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class ProjectsService
{
    public ApiResponse<object> View(ProjectAccessRequest request)
    {
        // Check if the project is confidential
        if (request.IsConfidentialProject)
        {
            // Check if the user is a project member or an administrator
            if (request.IsProjectMember || request.UserRole == "admin")
            {
                return ApiResponse<object>.Success(
                    new
                    {
                        accessGranted = true,
                        projectId = request.ProjectId
                    },
                    "Project details retrieved");
            }
            else
            {
                return ApiResponse<object>.Failure(
                    new List<string> { "Access denied" },
                    "Authorization failed");
            }
        }
        else
        {
            // Non-confidential projects remain accessible to authenticated users
            return ApiResponse<object>.Success(
                new
                {
                    accessGranted = true,
                    projectId = request.ProjectId
                },
                "Project details retrieved");
        }
    }
}
