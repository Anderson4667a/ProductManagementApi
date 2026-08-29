using System.Text.Json.Serialization;

namespace ProductManagement.Api.Models;

public class ExchangeRateResponse
{
    [JsonPropertyName("rates")]
    public Rates Rates { get; set; } = new();
}

public class Rates
{
    [JsonPropertyName("COP")]
    public decimal Cop { get; set; }
}