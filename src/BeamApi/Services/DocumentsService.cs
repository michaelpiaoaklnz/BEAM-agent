using BeamApi.Models.Requests;

namespace BeamApi.Services;

public class DocumentsService
{
    public DocumentLookupResult Lookup(DocumentLookupRequest request)
    {
        // No documents are currently stored; every lookup is treated as not-found.
        return new DocumentLookupResult { Found = false };
    }
}

public class DocumentLookupResult
{
    public bool Found { get; set; }
    public object? Document { get; set; }
}
