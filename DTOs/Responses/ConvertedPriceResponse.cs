namespace ProductManagement.Api.DTOs.Responses;

public class ConvertedPriceResponse
{
    public Int32 ProductId { get; set; }

    public String ProductName { get; set; } = string.Empty;

    public Decimal PriceUsd { get; set; }

    public Decimal ExchangeRate { get; set; }

    public Decimal PriceCop { get; set; }
}