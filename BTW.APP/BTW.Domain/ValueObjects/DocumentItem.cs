using CSharpFunctionalExtensions;

namespace BTW.Domain.ValueObjects;

public class DocumentItem
{
    public string Description { get; }
    public int Quantity { get; }
    public decimal UnitValue { get; }
    public decimal Subtotal => Quantity * UnitValue;

    private DocumentItem(string description, int quantity, decimal unitValue)
    {
        Description = description;
        Quantity = quantity;
        UnitValue = unitValue;
    }

    public static Result<DocumentItem> Create(string description, int quantity, decimal unitValue)
    {
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<DocumentItem>("Descripción requerida");

        if (quantity <= 0)
            return Result.Failure<DocumentItem>("Cantidad inválida");

        if (unitValue <= 0)
            return Result.Failure<DocumentItem>("Valor unitario inválido");

        return Result.Success(new DocumentItem(description, quantity, unitValue));
    }
}
