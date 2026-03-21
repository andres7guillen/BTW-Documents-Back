using BTW.Domain.Enums;

namespace BTW.Application.DTOs.Requests;

public class CreateDocumentRequest
{
    public string LegalNumber { get; set; } = string.Empty;

    public DocumentType Type { get; set; }

    public string EmitterNit { get; set; } = string.Empty;
    public string EmitterName { get; set; } = string.Empty;

    public string ReceiverNit { get; set; } = string.Empty;
    public string ReceiverName { get; set; } = string.Empty;

    public Guid? ReferenceDocumentId { get; set; }

    public List<CreateDocumentItemRequest> Items { get; set; } = new();
}
