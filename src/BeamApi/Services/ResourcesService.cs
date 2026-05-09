using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using System;

namespace BeamApi.Services;

public class ResourcesService
{
    public ApiResponse<object> Create(ResourceCreateRequest request)
    {
        var resourceId = $"resource-{Guid.NewGuid():N}";
        var location = $"/api/resources/{resourceId}";

        // Revised T25 behavior:
        // successful creation returns only the resource ID and location.
        return ApiResponse<object>.Success(
            new
            {
                id = resourceId,
                location = location
            },
            "Resource created");
    }
}
