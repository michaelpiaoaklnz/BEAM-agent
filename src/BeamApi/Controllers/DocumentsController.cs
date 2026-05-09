using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/documents")]
public class DocumentsController : ControllerBase
{
    private readonly DocumentsService _documentsService;

    public DocumentsController(DocumentsService documentsService)
    {
        _documentsService = documentsService;
    }

    [HttpPost("lookup")]
    public IActionResult Lookup(
        [FromBody] DocumentLookupRequest request)
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

            return Ok(ApiResponse<object>.Failure(
                errors,
                "Validation failed"));
        }

        var result = _documentsService.Lookup(request);

        if (!result.Found)
        {
            var traceId = string.IsNullOrWhiteSpace(HttpContext.TraceIdentifier)
                ? Guid.NewGuid().ToString()
                : HttpContext.TraceIdentifier;

            var notFoundResponse = new ApiResponse<object>
            {
                Succeeded = false,
                Message = "Document not found",
                Errors = new List<string> { "Document not found." },
                Data = new
                {
                    errorCode = "DOCUMENT_NOT_FOUND",
                    traceId
                }
            };

            return NotFound(notFoundResponse);
        }

        return Ok(ApiResponse<object>.Success(
            result.Document,
            "Document found"));
    }
}
