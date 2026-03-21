using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTW.Domain.Entities;

public class DocumentLog
{
    public Guid Id { get; private set; }
    public Guid DocumentId { get; private set; }
    public string Event { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private DocumentLog() { }

    public DocumentLog(Guid documentId, string @event)
    {
        Id = Guid.NewGuid();
        DocumentId = documentId;
        Event = @event;
        CreatedAt = DateTime.UtcNow;
    }
}
