using BTW.Application.Helpers;
using BTW.Domain.Entities;
using BTW.Domain.Enums;
using BTW.Domain.ValueObjects;
using CSharpFunctionalExtensions;

namespace BTW.Application.Services.DocumentService;

public interface IDocumentService
{
    Task<Result<Document>> CreateAsync(string legalNumber,DocumentType type,string emitterNit,string emitterName,string receiverNit,string receiverName,
        List<DocumentItem> items,Guid? referenceDocumentId);
    Task<Result> IssueAsync(Guid documentId);
    Task<Result> CancelAsync(Guid documentId);
    Task<Maybe<Document>> GetByIdAsync(Guid id);
    Task<Result<PagedResult<Document>>> GetAllAsync(int page, int pageSize);
}