using BTW.Domain.Enums;
using BTW.Domain.ValueObjects;
using CSharpFunctionalExtensions;

namespace BTW.Domain.Entities;

public class Document
{
    public Guid Id { get; private set; }
    public string LegalNumber { get; private set; }
    public Guid? Cufe { get; private set; }
    public DocumentType Type { get; private set; }
    public DocumentStatus Status { get; private set; }
    public string EmitterNit { get; private set; }
    public string EmitterName { get; private set; }
    public string ReceiverNit { get; private set; }
    public string ReceiverName { get; private set; }
    public Guid? ReferenceDocumentId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? IssuedAt { get; private set; }
    private readonly List<DocumentItem> _items = new();
    public IReadOnlyCollection<DocumentItem> Items => _items;
    public decimal Total => _items.Sum(x => x.Subtotal);
    private Document() { }
    private Document(
        string legalNumber,
        DocumentType type,
        string emitterNit,
        string emitterName,
        string receiverNit,
        string receiverName,
        Guid? referenceDocumentId)
    {
        Id = Guid.NewGuid();
        LegalNumber = legalNumber;
        Type = type;
        Status = DocumentStatus.Draft;
        EmitterNit = emitterNit;
        EmitterName = emitterName;
        ReceiverNit = receiverNit;
        ReceiverName = receiverName;
        ReferenceDocumentId = referenceDocumentId;
        CreatedAt = DateTime.UtcNow;
    }

    public static Result<Document> Create(
        string legalNumber,
        DocumentType type,
        string emitterNit,
        string emitterName,
        string receiverNit,
        string receiverName,
        List<DocumentItem> items,
        Guid? referenceDocumentId = null)
    {
        if (string.IsNullOrWhiteSpace(legalNumber))
            return Result.Failure<Document>("Número legal requerido");

        if (string.IsNullOrWhiteSpace(emitterNit))
            return Result.Failure<Document>("NIT emisor requerido");

        if (string.IsNullOrWhiteSpace(receiverNit))
            return Result.Failure<Document>("NIT receptor requerido");

        if (items == null || !items.Any())
            return Result.Failure<Document>("Debe tener al menos un item");

        if (type == DocumentType.CreditNote && referenceDocumentId == null)
            return Result.Failure<Document>("La nota crédito debe referenciar una factura");

        var doc = new Document(
            legalNumber,
            type,
            emitterNit,
            emitterName,
            receiverNit,
            receiverName,
            referenceDocumentId
        );

        foreach (var item in items)
        {
            doc._items.Add(item);
        }

        if (doc.Total <= 0)
            return Result.Failure<Document>("Total inválido");

        return Result.Success(doc);
    }

    public Result AddItem(string description, int quantity, decimal unitValue)
    {
        if (Status != DocumentStatus.Draft)
            return Result.Failure("No se pueden modificar documentos emitidos o anulados");

        var itemResult = DocumentItem.Create(description, quantity, unitValue);

        if (itemResult.IsFailure)
            return Result.Failure(itemResult.Error);

        _items.Add(itemResult.Value);

        return Result.Success();
    }

    public Result Issue()
    {
        if (Status != DocumentStatus.Draft)
            return Result.Failure("Solo documentos en borrador pueden emitirse");

        if (!_items.Any())
            return Result.Failure("No se puede emitir sin items");

        if (CreatedAt.Date != DateTime.UtcNow.Date)
            return Result.Failure("Solo se pueden emitir documentos del mismo día");

        Status = DocumentStatus.Issued;
        IssuedAt = DateTime.UtcNow;
        Cufe = Guid.NewGuid();

        return Result.Success();
    }

    public Result Cancel()
    {
        if (Status != DocumentStatus.Issued)
            return Result.Failure("Solo documentos emitidos pueden anularse");

        Status = DocumentStatus.Cancelled;

        return Result.Success();
    }
}
