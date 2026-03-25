using BTW.Application.Helpers;
using BTW.Domain.Entities;
using BTW.Domain.Enums;
using CSharpFunctionalExtensions;

namespace BTW.Application.Services.DocumentHistory;

public interface IDocumentHistoryService
{
    Task<Result> AddAsync(Guid documentId, DocumentStatus status);
    Task<Result<PagedResult<DocumentStatusHistory>>> GetLogAsync(Guid documentId, int page, int pageSize);
}