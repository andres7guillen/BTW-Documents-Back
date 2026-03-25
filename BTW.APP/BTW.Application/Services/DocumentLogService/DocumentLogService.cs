using BTW.Application.Helpers;
using BTW.Domain.Entities;
using BTW.Domain.Repositories;
using CSharpFunctionalExtensions;

namespace BTW.Application.Services.DocumentLogService;

public class DocumentLogService : IDocumentLogService
{
    private readonly IDocumentLogRepository _repository;

    public DocumentLogService(IDocumentLogRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<DocumentLog>> AddLogAsync(Guid documentId, string status)
    {
        return await _repository.AddLogAsync(documentId, status);
    }

    public async Task<Result<PagedResult<DocumentLog>>> GetLogAsync(Guid documentId, int page, int pageSize, string @event) 
    { 
        var result = await _repository.GetLogAsync(documentId, page, pageSize, @event);
        return new PagedResult<DocumentLog>
        {
            Items = result.Value.Item1,
            TotalCount = result.Value.Item2,
            Page = page,
            PageSize = pageSize
        };
    }

}
