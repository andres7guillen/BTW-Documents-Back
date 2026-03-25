using BTW.Application.Services.DocumentHistory;
using BTW.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BTW.API.Controllers;

[ApiController]
[Route("api/documentHistory")]
public class DocumentHistoryController : ControllerBase
{
    private readonly IDocumentHistoryService _service;

    public DocumentHistoryController(IDocumentHistoryService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        Guid documentId,
        [FromBody] DocumentStatus status)
    {
        var result = await _service.AddAsync(documentId, status);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok();
    }

    [HttpGet("{documentId}")]
    public async Task<IActionResult> Get(
    Guid documentId,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        var result = await _service.GetLogAsync(documentId, page, pageSize);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
