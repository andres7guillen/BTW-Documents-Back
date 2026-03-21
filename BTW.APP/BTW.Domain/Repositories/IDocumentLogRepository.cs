using CSharpFunctionalExtensions;

namespace BTW.Domain.Repositories;

public interface IDocumentLogRepository
{
    Task<Result> LogAsync(Guid documentId, string @event);
}
