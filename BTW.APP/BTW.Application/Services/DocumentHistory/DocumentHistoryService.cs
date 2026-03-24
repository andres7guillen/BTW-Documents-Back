using BTW.Application.Helpers;
using BTW.Domain.Entities;
using BTW.Domain.Enums;
using BTW.Domain.Repositories;
using CSharpFunctionalExtensions;

namespace BTW.Application.Services.DocumentHistory;

public class DocumentHistoryService : IDocumentHistoryService
{
    private readonly IDocumentHistoryRepository _repository;

    public DocumentHistoryService(IDocumentHistoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> AddAsync(Guid documentId, DocumentStatus status)
    {
        return await _repository.AddAsync(documentId, status);
    }

    public async Task<Result<PagedResult<DocumentStatusHistory>>> GetLogAsync(Guid documentId, int page, int pageSize, string @event)
    {
        var result = await _repository.GetLogAsync(documentId, page, pageSize, @event);
        return new PagedResult<DocumentStatusHistory>
        {
            Items = result.Value.Item1,
            TotalCount = result.Value.Item2,
            Page = page,
            PageSize = pageSize
        };
    }
}
