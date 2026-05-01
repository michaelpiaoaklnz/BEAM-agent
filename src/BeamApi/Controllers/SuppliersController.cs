using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly SuppliersService _suppliersService;

    public SuppliersController(SuppliersService suppliersService)
    {
        _suppliersService = suppliersService;
    }

    [HttpPost("onboard")]
    public ActionResult<ApiResponse<string>> Onboard([FromBody] SupplierOnboardRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Invalid input." : e.ErrorMessage)
                .ToList();

            return UnprocessableEntity(ApiResponse<string>.Failure(errors, "Validation failed"));
        }

        var result = _suppliersService.Onboard(request);

        if (!result.Succeeded)
            return UnprocessableEntity(result);

        return Ok(result);
    }
}
