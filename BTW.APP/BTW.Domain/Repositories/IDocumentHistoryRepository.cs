using BTW.Domain.Enums;
using CSharpFunctionalExtensions;

namespace BTW.Domain.Repositories;

public interface IDocumentHistoryRepository
{
    Task<Result> AddAsync(Guid documentId, DocumentStatus status);
}
