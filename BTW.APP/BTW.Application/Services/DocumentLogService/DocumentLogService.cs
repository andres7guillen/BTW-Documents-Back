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

    public async Task<Result> LogAsync(Guid documentId, string @event) => await _repository.LogAsync(documentId, @event);
}
