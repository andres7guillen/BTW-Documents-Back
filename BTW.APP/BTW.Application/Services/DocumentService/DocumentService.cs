using BTW.Application.Context;
using BTW.Application.Services.DocumentHistory;
using BTW.Application.Services.DocumentLogService;
using BTW.Domain.Entities;
using BTW.Domain.Enums;
using BTW.Domain.Repositories;
using BTW.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTW.Application.Services.DocumentService;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _repository;
    private readonly IDocumentLogService _logService;
    private readonly IDocumentHistoryService _historyService;

    public DocumentService(
        IDocumentRepository repository,
        IDocumentLogService logService,
        IDocumentHistoryService historyService)
    {
        _repository = repository;
        _logService = logService;
        _historyService = historyService;
    }

    public async Task<Result<Document>> CreateAsync(string legalNumber,DocumentType type,string emitterNit,string emitterName,string receiverNit,
        string receiverName,List<DocumentItem> items,Guid? referenceDocumentId)
    {
        var existing = await _repository.GetByLegalNumberAsync(legalNumber);
        if (existing.HasValue)
            return Result.Failure<Document>("Ya existe un documento con ese número legal");

        if (type == DocumentType.CreditNote)
        {
            var referenced = await _repository.GetByIdAsync(referenceDocumentId!.Value);

            if (referenced.HasNoValue)
                return Result.Failure<Document>("La factura referenciada no existe");

            if (referenced.Value.Status != DocumentStatus.Issued)
                return Result.Failure<Document>("La factura debe estar emitida");
        }

        var docResult = Document.Create(legalNumber,type,emitterNit,emitterName,receiverNit,receiverName,items,referenceDocumentId);

        if (docResult.IsFailure)
            return docResult;

        var addResult = await _repository.AddAsync(docResult.Value);
        if (addResult.IsFailure)
            return Result.Failure<Document>(addResult.Error);

        await _logService.LogAsync(addResult.Value.Id, "DOCUMENT_CREATED");
        await _historyService.AddAsync(addResult.Value.Id, addResult.Value.Status);

        return addResult;
    }

    public async Task<Result> IssueAsync(Guid documentId)
    {
        var doc = await _repository.GetByIdAsync(documentId);
        if (doc.HasNoValue)
            return Result.Failure("Documento no encontrado");

        var result = doc.Value.Issue();
        if (result.IsFailure)
            return result;

        var updateResult = await _repository.UpdateDocumentAsync(doc.Value);
        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Error);

        await _logService.LogAsync(doc.Value.Id, "DOCUMENT_ISSUED");
        await _historyService.AddAsync(doc.Value.Id, doc.Value.Status);

        return Result.Success();
    }

    public async Task<Result> CancelAsync(Guid documentId)
    {
        var doc = await _repository.GetByIdAsync(documentId);
        if (doc.HasNoValue)
            return Result.Failure("Documento no encontrado");

        var result = doc.Value.Cancel();
        if (result.IsFailure)
            return result;

        var updateResult = await _repository.UpdateDocumentAsync(doc.Value);
        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Error);

        await _logService.LogAsync(doc.Value.Id, "DOCUMENT_CANCELLED");
        await _historyService.AddAsync(doc.Value.Id, doc.Value.Status);

        return Result.Success();
    }

    public async Task<Maybe<Document>> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Result<List<Document>>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
}
