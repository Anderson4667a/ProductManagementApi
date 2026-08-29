namespace ProductManagement.Api.Services.Interfaces;

public interface IExchangeRateService
{
    Task<Decimal> GetUsdToCopRateAsync();
}