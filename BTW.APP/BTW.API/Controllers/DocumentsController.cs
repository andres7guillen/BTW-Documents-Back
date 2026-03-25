using BTW.Application.DTOs.Requests;
using BTW.Application.Services.DocumentService;
using BTW.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace BTW.API.Controllers;

[ApiController]
[Route("api/documents")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentService _service;

    public DocumentsController(IDocumentService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDocumentRequest request)
    {
        var items = request.Items
            .Select(x => DocumentItem.Create(x.Description, x.Quantity, x.UnitValue))
            .ToList();

        if (items.Any(x => x.IsFailure))
            return BadRequest(items.First(x => x.IsFailure).Error);

        var domainItems = items.Select(x => x.Value).ToList();

        var result = await _service.CreateAsync(
            request.LegalNumber,
            request.Type,
            request.EmitterNit,
            request.EmitterName,
            request.ReceiverNit,
            request.ReceiverName,
            domainItems,
            request.ReferenceDocumentId
        );

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("{id}/issue")]
    public async Task<IActionResult> Issue(Guid id)
    {
        var result = await _service.IssueAsync(id);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var result = await _service.CancelAsync(id);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result.HasNoValue)
            return NotFound();

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string? status = null,
    [FromQuery] string? type = null)
    {
        var docs = await _service.GetAllAsync(page, pageSize, status, type);

        return docs.IsSuccess
            ? Ok(docs.Value)
            : BadRequest(docs.Error);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) 
    {
        var result = await _service.Delete(id);
        return result.IsSuccess
            ? Ok() : BadRequest(result.Error);
    }
}
