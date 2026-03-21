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

    public async Task<Maybe<Document>> GetByLegalNumberAsync(string legalNumber)
    {
        var document = await _context.Documents
            .FirstOrDefaultAsync(x => x.LegalNumber == legalNumber);
        return document == null
            ? Maybe.None 
            : Maybe.From(document);
    }

    public async Task<Result<List<Document>>> GetAllAsync()
    {
        var documents = await _context.Documents
            .Include(x => x.Items)
            .ToListAsync();
        return Result.Success(documents);
    }

    public async Task<Result<bool>> UpdateDocumentAsync(Document document)
    {
        _context.Documents.Update(document);
        return await _context.SaveChangesAsync() > 0
            ? Result.Success(true)
            : Result.Failure<bool>("Error actualizando el documento");
    }
}
