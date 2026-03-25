using BTW.Domain.Entities;
using BTW.Domain.Enums;
using CSharpFunctionalExtensions;

namespace BTW.Domain.Repositories;

public interface IDocumentHistoryRepository
{
    Task<Result> AddAsync(Guid documentId, DocumentStatus status);
    Task<Result<(IEnumerable<DocumentStatusHistory>, int totalCount)>> GetLogAsync(Guid documentId, int page, int pageSize);
}
