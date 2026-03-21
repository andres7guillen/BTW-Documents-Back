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
}
