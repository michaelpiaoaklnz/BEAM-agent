using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class SearchService
{
    public ApiResponse<List<string>> Search(SearchRequest request)
    {
        // Handle empty, null, or missing keyword by returning all records
        if (string.IsNullOrWhiteSpace(request?.Keyword))
        {
            var results = new List<string>
            {
                "record-1",
                "record-2",
                "record-3"
            };

            return ApiResponse<List<string>>.Success(
                results,
                "Search completed");
        }

        // Handle non-empty keyword
        var filteredResults = new List<string>
        {
            "record-1",
            "record-2",
            "record-3"
        }.Where(r => r.Contains(request.Keyword, StringComparison.OrdinalIgnoreCase)).ToList();

        return ApiResponse<List<string>>.Success(
            filteredResults,
            "Search completed");
    }
}
