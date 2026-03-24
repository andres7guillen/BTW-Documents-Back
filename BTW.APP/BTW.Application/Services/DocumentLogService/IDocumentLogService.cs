using BTW.Application.Helpers;
using BTW.Domain.Entities;
using CSharpFunctionalExtensions;

namespace BTW.Application.Services.DocumentLogService;

public interface IDocumentLogService
{
    Task<Result<PagedResult<DocumentLog>>> GetLogAsync(Guid documentId, int page, int pageSize, string @event);
    Task<Result<DocumentLog>> AddLogAsync(Guid documentId, string status);
}
