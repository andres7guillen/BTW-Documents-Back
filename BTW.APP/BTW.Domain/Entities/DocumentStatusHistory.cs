using BTW.Domain.Enums;

namespace BTW.Domain.Entities;

public class DocumentStatusHistory
{
    public Guid Id { get; private set; }
    public Guid DocumentId { get; private set; }
    public DocumentStatus Status { get; private set; }
    public DateTime ChangedAt { get; private set; }
    private DocumentStatusHistory() { }

    public DocumentStatusHistory(Guid documentId, DocumentStatus status)
    {
        Id = Guid.NewGuid();
        DocumentId = documentId;
        Status = status;
        ChangedAt = DateTime.UtcNow;
    }
}
