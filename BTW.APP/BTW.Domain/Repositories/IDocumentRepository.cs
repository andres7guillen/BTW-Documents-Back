using BTW.Domain.Entities;
using CSharpFunctionalExtensions;

namespace BTW.Domain.Repositories;

public interface IDocumentRepository
{
    Task<Result<Document>> AddAsync(Document document);
    Task<Result<bool>> UpdateDocumentAsync(Document document);
    Task<Maybe<Document>> GetByIdAsync(Guid id);
    Task<Result<bool>> ExistsDocumentByLegalNumberAsync(string legalNumber);
    Task<Result<List<Document>>> GetAllAsync();
}
