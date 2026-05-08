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
        if (request == null)
        {
            request = new SearchRequest();
        }

        var result = _searchService.Search(request);

        return Ok(result);
    }
}
