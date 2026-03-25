using BTW.Domain.Entities;
using CSharpFunctionalExtensions;

namespace BTW.Domain.Repositories;

public interface IDocumentLogRepository
{
    Task<Result<DocumentLog>> AddLogAsync(Guid documentId, string @event);
    Task<Result<(IEnumerable<DocumentLog>, int totalCount)>> GetLogAsync(Guid documentId, int page, int pageSize, string @event);
}
