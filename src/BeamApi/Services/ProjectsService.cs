using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class ProjectsService
{
    public ApiResponse<object> View(ProjectAccessRequest request)
    {
        // Original T24 behavior:
        // any authenticated user can view project details.

        return ApiResponse<object>.Success(
            new
            {
                accessGranted = true,
                projectId = request.ProjectId
            },
            "Project details retrieved");
    }
}
