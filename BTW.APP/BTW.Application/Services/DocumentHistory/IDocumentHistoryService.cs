using BTW.Domain.Enums;
using CSharpFunctionalExtensions;

namespace BTW.Application.Services.DocumentHistory;

public interface IDocumentHistoryService
{
    Task<Result> AddAsync(Guid documentId, DocumentStatus status);
}