using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using System.Diagnostics;

namespace BeamApi.Services;

public class DocumentsService
{
    public ApiResponse<object> Lookup(DocumentLookupRequest request)
    {
        // Simulate a not-found scenario
        if (request.DocumentId == "missing-doc")
        {
            var traceId = Activity.Current?.Id ?? Guid.NewGuid().ToString();
            return ApiResponse<object>.Failure(
                new List<string> { "Document not found." },
                "Not Found",
                new { errorCode = "NOT_FOUND", traceId = traceId });
        }

        // Original behavior for found documents
        return ApiResponse<object>.Success(new { }, "Document found");
    }
}
