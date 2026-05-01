using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class AuditService
{
    public ApiResponse<object> Process(AuditProcessRequest request)
    {
        // Original T12 baseline behavior:
        // audit includes only userId and timestamp.
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
