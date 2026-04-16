using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using System;
using System.Collections.Concurrent;

namespace BeamApi.Services;

public class CaseService
{
    private static readonly ConcurrentDictionary<string, ApiResponse<object>> _idempotencyCache = new();

    public ApiResponse<object> Create(CaseCreateRequest request)
    {
        if (_idempotencyCache.TryGetValue(request.IdempotencyKey, out var cachedResponse))
        {
            return cachedResponse;
        }

        var result = new
        {
            caseId = $"case-{Guid.NewGuid():N}",
            customerId = request.CustomerId,
            title = request.Title,
            description = request.Description,
            status = "Created",
            idempotencyKey = request.IdempotencyKey
        };

        var response = ApiResponse<object>.Success(result, "Case created successfully");
        _idempotencyCache.TryAdd(request.IdempotencyKey, response);

        return response;
    }
}
