using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class SearchService
{
    public ApiResponse<List<string>> Search(SearchRequest request)
    {
        // Original T17 behavior:
        // empty keyword returns all records.

        // Original T18 behavior:
        // missing pagination parameters use a fixed default page size.
        var page = request.Page ?? 1;
        var pageSize = request.PageSize ?? 20;

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
}