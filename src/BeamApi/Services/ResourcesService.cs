using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class ResourcesService
{
    public ApiResponse<object> Create(ResourceCreateRequest request)
    {
        var resourceId = $"resource-{Guid.NewGuid():N}";

        // Original T25 behavior:
        // successful creation returns the full entity object.
        return ApiResponse<object>.Success(
            new
            {
                id = resourceId,
                name = request.Name,
                type = request.Type,
                created = true
            },
            "Resource created");
    }
}