using BTW.Application.Context;
using BTW.Domain.Entities;
using BTW.Domain.Enums;
using BTW.Domain.Repositories;
using CSharpFunctionalExtensions;

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
}
