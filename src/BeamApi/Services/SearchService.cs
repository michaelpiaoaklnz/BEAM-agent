using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class SearchService
{
    public ApiResponse<List<string>> Search(SearchRequest request)
    {
        // Original T17 behavior:
        // empty keyword returns all records.

        // T18 perturbed behavior:
        // missing pageSize uses a role-specific default; unknown/missing roles fall back to 20.
        var page = request.Page ?? 1;
        var pageSize = request.PageSize ?? GetDefaultPageSizeForRole(request.UserRole);

        var allRecords = Enumerable
            .Range(1, 100)
            .Select(i => $"record-{i}")
            .ToList();

        var results = allRecords
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return ApiResponse<List<string>>.Success(
            results,
            $"Search completed with page={page}, pageSize={pageSize}");
    }

    private static int GetDefaultPageSizeForRole(string? userRole)
    {
        return userRole?.Trim().ToLowerInvariant() switch
        {
            "admin" => 100,
            "manager" => 50,
            _ => 20,
        };
    }
}