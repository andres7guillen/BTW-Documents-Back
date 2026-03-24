using BTW.Application.Services.DocumentLogService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTW.API.Controllers;

[ApiController]
[Route("api/documents/{documentId:guid}/logs")]
public class DocumentLogController : ControllerBase
{
    private readonly IDocumentLogService _service;

    public DocumentLogController(IDocumentLogService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        Guid documentId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery(Name = "event")] string? @event = null)
    {
        var result = await _service.GetLogAsync(documentId, page, pageSize, @event ?? string.Empty);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
