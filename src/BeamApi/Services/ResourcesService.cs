using BeamApi.Models.Requests;
using System;

namespace BeamApi.Services;

public class ResourcesService
{
    public (string Id, string Location) Create(ResourceCreateRequest request)
    {
        var resourceId = $"resource-{Guid.NewGuid():N}";
        var location = $"/api/resources/{resourceId}";
        return (resourceId, location);
    }
}
