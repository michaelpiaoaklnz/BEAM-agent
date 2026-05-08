using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class SearchService
{
    public ApiResponse<List<string>> Search(SearchRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Keyword))
        {
            return ApiResponse<List<string>>.Failure(
                new List<string> { "Keyword is required" },
                "Validation failed");
        }

        // Original T17 behavior:
        // empty keyword returns all records.

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
}
