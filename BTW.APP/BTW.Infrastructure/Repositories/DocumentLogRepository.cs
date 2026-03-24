using BTW.Application.Context;
using BTW.Domain.Entities;
using BTW.Domain.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace BTW.Infrastructure.Repositories;

public class DocumentLogRepository : IDocumentLogRepository
{
    private readonly AppDbContext _context;

    public DocumentLogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result> AddLogAsync(Guid documentId, string @event)
    {
        var log = new DocumentLog(documentId, @event);

        await _context.DocumentLogs.AddAsync(log);
        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<(IEnumerable<DocumentLog>, int totalCount)>> GetLogAsync(Guid documentId, int page, int pageSize, string @event)
    {
        var query = _context.DocumentLogs.AsQueryable();

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var result = (items, totalCount);

        return Result.Success<(IEnumerable<DocumentLog>, int)>(result);
    }
}
