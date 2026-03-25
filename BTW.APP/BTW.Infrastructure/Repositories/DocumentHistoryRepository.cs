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

    public async Task<Result<(IEnumerable<DocumentStatusHistory>, int totalCount)>> GetLogAsync(Guid documentId, int page, int pageSize)
    {
        var query = _context.DocumentStatusHistories.AsQueryable();
        var items = await query
            .Where(c => c.DocumentId == documentId)
            .OrderByDescending(x => x.ChangedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalCount = await query
            .Where(c => c.DocumentId == documentId)
            .CountAsync();

        var result = (items, totalCount);

        return Result.Success<(IEnumerable<DocumentStatusHistory>, int)>(result);
    }
}
