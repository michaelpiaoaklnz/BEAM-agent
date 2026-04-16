using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CasesController : ControllerBase
{
    private readonly CaseService _caseService;

    public CasesController(CaseService caseService)
    {
        _caseService = caseService;
    }

    [HttpPost("create")]
    public ActionResult<ApiResponse<object>> Create([FromBody] CaseCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Invalid input." : e.ErrorMessage)
                .ToList();

            return UnprocessableEntity(ApiResponse<object>.Failure(errors, "Validation failed"));
        }

        var result = _caseService.Create(request);
        return Ok(result);
    }
}