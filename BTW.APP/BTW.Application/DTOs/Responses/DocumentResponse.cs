namespace BTW.Application.DTOs.Responses;

public class DocumentResponse
{
    public Guid Id { get; set; }
    public string LegalNumber { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public decimal Total { get; set; }
}
