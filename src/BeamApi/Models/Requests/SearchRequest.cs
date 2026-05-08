using System.ComponentModel.DataAnnotations;

namespace BeamApi.Models.Requests;

public class SearchRequest
{
    [Required(ErrorMessage = "Keyword is required")]
    public string? Keyword { get; set; }
}
