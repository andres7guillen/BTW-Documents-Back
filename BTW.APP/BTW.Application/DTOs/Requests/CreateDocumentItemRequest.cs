namespace BTW.Application.DTOs.Requests;

public class CreateDocumentItemRequest
{
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitValue { get; set; }
}
