namespace BeamApi.Models.Requests;

public class SearchRequest
{
    public string? Keyword { get; set; }

    public int? Page { get; set; }

    public int? PageSize { get; set; }

    public string? UserRole { get; set; }
}