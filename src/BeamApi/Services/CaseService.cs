using System.Collections.Concurrent;
using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class CaseService
{
    private readonly ConcurrentDictionary<string, ApiResponse<object>> _idempotencyCache = new();

    public ApiResponse<object> Create(CaseCreateRequest request)
    {
        return _idempotencyCache.GetOrAdd(request.IdempotencyKey, _ =>
        {
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
        });
    }
}