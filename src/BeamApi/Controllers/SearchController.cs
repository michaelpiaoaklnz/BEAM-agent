using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly SearchService _searchService;

    public SearchController(SearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpPost]
    public ActionResult<ApiResponse<List<string>>> Search(
        [FromBody] SearchRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage)
                    ? "Invalid input."
                    : e.ErrorMessage)
                .ToList();

            return Ok(ApiResponse<List<string>>.Failure(
                errors,
                "Validation failed"));
        }

        var result = _searchService.Search(request);

        return Ok(result);
    }
}