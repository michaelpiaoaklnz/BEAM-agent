using System.ComponentModel.DataAnnotations;

namespace BeamApi.Models.Requests;

public class SearchRequest
{
    public string? Keyword { get; set; }
}