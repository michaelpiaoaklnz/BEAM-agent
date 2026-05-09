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
        [FromBody] SearchRequest? request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Keyword))
        {
            var errors = new List<string> { "keyword is required." };
            return BadRequest(ApiResponse<List<string>>.Failure(errors, "keyword is required."));
        }

        var result = _searchService.Search(request);

        return Ok(result);
    }
}
