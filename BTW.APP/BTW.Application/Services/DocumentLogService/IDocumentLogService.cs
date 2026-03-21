using CSharpFunctionalExtensions;

namespace BTW.Application.Services.DocumentLogService;

public interface IDocumentLogService
{
    Task<Result> LogAsync(Guid documentId, string @event);
}
