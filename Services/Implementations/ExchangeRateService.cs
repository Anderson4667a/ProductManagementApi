using System.Text.Json;
using ProductManagement.Api.Models;
using ProductManagement.Api.Services.Interfaces;

namespace ProductManagement.Api.Services.Implementations;

public class ExchangeRateService : IExchangeRateService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ExchangeRateService> _logger;

    public ExchangeRateService(
        HttpClient httpClient,
        ILogger<ExchangeRateService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<decimal> GetUsdToCopRateAsync()
    {
        _logger.LogInformation(
            "Consultando tasa de cambio USD a COP");

        HttpResponseMessage response =
            await _httpClient.GetAsync("https://open.er-api.com/v6/latest/USD");

        response.EnsureSuccessStatusCode();

        string jsonResponse =
            await response.Content.ReadAsStringAsync();

        ExchangeRateResponse? exchangeRateResponse =
            JsonSerializer.Deserialize<ExchangeRateResponse>(
                jsonResponse);

        if (exchangeRateResponse != null)
        {
            _logger.LogInformation("Tasa de cambio obtenida correctamente: {ExchangeRate}", exchangeRateResponse.Rates.Cop);
        }
        else
        {
            throw new Exception("No fue posible obtener la tasa de cambio.");
        }


        return exchangeRateResponse.Rates.Cop;
    }
}