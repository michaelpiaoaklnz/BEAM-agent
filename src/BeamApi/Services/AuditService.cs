using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using System;

namespace BeamApi.Services;

public class AuditService
{
    public ApiResponse<object> Process(AuditProcessRequest request)
    {
        // Enriched audit details
        var result = new
        {
            processed = true,
            audit = new
            {
                userId = request.UserId,
                timestamp = DateTime.UtcNow,
                actionName = request.ActionName,
                entityType = request.EntityType,
                entityId = request.EntityId,
                outcome = "Success"
            }
        };

        return ApiResponse<object>.Success(result, "Audit processed successfully");
    }
}
