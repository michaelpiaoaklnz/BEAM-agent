using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using System.Collections.Generic;
using System.Linq;

namespace BeamApi.Services;

public class SearchService
{
    public ApiResponse<List<string>> Search(SearchRequest request)
    {
        // Simulate fetching all records
        var allRecords = Enumerable
            .Range(1, 100)
            .Select(i => $"record-{i}")
            .ToList();

        // Apply pagination
        var results = allRecords
            .Skip((request.Page.Value - 1) * request.PageSize.Value)
            .Take(request.PageSize.Value)
            .ToList();

        // Return the paginated results
        return ApiResponse<List<string>>.Success(
            results,
            $"Search completed with page={request.Page}, pageSize={request.PageSize}");
    }
}
