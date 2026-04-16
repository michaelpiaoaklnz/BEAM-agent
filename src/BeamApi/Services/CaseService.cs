using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using System;

namespace BeamApi.Services;

public class CaseService
{
    public ApiResponse<object> Create(CaseCreateRequest request)
    {
        // Original T09 baseline behavior:
        // duplicate requests may create multiple distinct records.
        var result = new
        {
            caseId = $"case-{Guid.NewGuid():N}",
            customerId = request.CustomerId,
            title = request.Title,
            description = request.Description,
            status = "Created",
            idempotencyKey = request.IdempotencyKey
        };

        return ApiResponse<object>.Success(result, "Case created successfully");
    }
}
