using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class DocumentsService
{
    public ApiResponse<object> Lookup(DocumentLookupRequest request)
    {
        // Original T26 behavior:
        // not-found requests return generic error text.

        return ApiResponse<object>.Failure(
            new List<string> { "Document not found." },
            "Generic error");
    }
}