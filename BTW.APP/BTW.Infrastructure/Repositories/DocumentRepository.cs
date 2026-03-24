using BTW.Application.Context;
using BTW.Domain.Entities;
using BTW.Domain.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace BTW.Infrastructure.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly AppDbContext _context;

    public DocumentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Document>> AddAsync(Document document)
    {
        await _context.Documents.AddAsync(document);
        await _context.SaveChangesAsync();
        return Result.Success(document);
    }

    public async Task<Maybe<Document>> GetByIdAsync(Guid id)
    {
        var document = await _context.Documents
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id);
        return document == null
            ? Maybe.None
            : Maybe.From(document);
    }

    public async Task<Result<bool>> ExistsDocumentByLegalNumberAsync(string legalNumber)
    {
        return await _context.Documents
            .AnyAsync(x => x.LegalNumber == legalNumber);       
    }

    public async Task<Result<(IEnumerable<Document>, int totalCount)>> GetAllAsync(int page, int pageSize)
    {
        var query = _context.Documents.AsQueryable();

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var result = (items, totalCount);

        return Result.Success<(IEnumerable<Document>, int)>(result);
    }

    public async Task<Result<bool>> UpdateDocumentAsync(Document document)
    {
        _context.Documents.Update(document);
        return await _context.SaveChangesAsync() > 0
            ? Result.Success(true)
            : Result.Failure<bool>("Error actualizando el documento");
    }
}
