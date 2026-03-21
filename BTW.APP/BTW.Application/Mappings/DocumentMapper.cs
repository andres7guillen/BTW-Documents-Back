using BTW.Application.DTOs.Responses;
using BTW.Domain.Entities;

namespace BTW.Application.Mappings;

public static class DocumentMapper
{
    public static DocumentResponse ToResponse(Document doc)
    {
        return new DocumentResponse
        {
            Id = doc.Id,
            LegalNumber = doc.LegalNumber,
            Type = doc.Type.ToString(),
            Status = doc.Status.ToString(),
            Total = doc.Total
        };
    }
}
