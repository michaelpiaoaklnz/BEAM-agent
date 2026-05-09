using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/resources")]
public class ResourcesController : ControllerBase
{
    private readonly ResourcesService _resourcesService;

    public ResourcesController(ResourcesService resourcesService)
    {
        _resourcesService = resourcesService;
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] ResourceCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Values
                .SelectMany(v => v.Errors)
                .Select(e =>
                    string.IsNullOrWhiteSpace(e.ErrorMessage)
                        ? "Invalid input."
                        : e.ErrorMessage)
                .ToList();

            return Ok(ApiResponse<object>.Failure(errors, "Validation failed"));
        }

        var (id, location) = _resourcesService.Create(request);
        return Created(location, new { id, location });
    }
}
