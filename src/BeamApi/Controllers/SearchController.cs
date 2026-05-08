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

        // Ensure page and pageSize are positive integers
        if (request.Page.HasValue && request.Page <= 0)
        {
            return BadRequest(ApiResponse<List<string>>.Failure(
                new List<string> { "Page must be a positive integer." },
                "Invalid input"));
        }

        if (request.PageSize.HasValue && request.PageSize <= 0)
        {
            return BadRequest(ApiResponse<List<string>>.Failure(
                new List<string> { "PageSize must be a positive integer." },
                "Invalid input"));
        }

        // Set default values for pagination parameters
        var page = request.Page ?? 1;
        var pageSize = request.PageSize ?? GetDefaultPageSize(request.UserRole);

        // Update the request object with default values
        request.Page = page;
        request.PageSize = pageSize;

        var result = _searchService.Search(request);

        return Ok(result);
    }

    private int GetDefaultPageSize(string? userRole)
    {
        switch (userRole?.ToLower())
        {
            case "admin":
                return 100;
            case "manager":
                return 50;
            case "user":
                return 20;
            default:
                return 20;
        }
    }
}
