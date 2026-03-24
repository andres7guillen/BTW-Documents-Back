using BTW.Application.Context;
using BTW.Domain.Entities;
using BTW.Domain.Enums;
using BTW.Domain.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace BTW.Infrastructure.Repositories;

public class DocumentHistoryRepository : IDocumentHistoryRepository
{
    private readonly AppDbContext _context;

    public DocumentHistoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result> AddAsync(Guid documentId, DocumentStatus status)
    {
        var history = new DocumentStatusHistory(documentId, status);

        await _context.DocumentStatusHistories.AddAsync(history);
        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<(IEnumerable<DocumentStatusHistory>, int totalCount)>> GetLogAsync(Guid documentId, int page, int pageSize, string @event)
    {
        var query = _context.DocumentStatusHistories.AsQueryable();

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.ChangedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var result = (items, totalCount);

        return Result.Success<(IEnumerable<DocumentStatusHistory>, int)>(result);
    }
}
