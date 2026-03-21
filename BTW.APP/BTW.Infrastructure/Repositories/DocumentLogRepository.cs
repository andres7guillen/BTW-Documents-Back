using BTW.Application.Context;
using BTW.Domain.Entities;
using BTW.Domain.Repositories;
using CSharpFunctionalExtensions;

namespace BTW.Infrastructure.Repositories;

public class DocumentLogRepository : IDocumentLogRepository
{
    private readonly AppDbContext _context;

    public DocumentLogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result> LogAsync(Guid documentId, string @event)
    {
        var log = new DocumentLog(documentId, @event);

        await _context.DocumentLogs.AddAsync(log);
        await _context.SaveChangesAsync();

        return Result.Success();
    }
}
