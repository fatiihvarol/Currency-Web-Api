using CurrencyApplication.ApplicationConfig;
using CurrencyApplication.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CurrencyApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CurrencyController : ControllerBase
{
    
    [HttpGet("All")]
    public  async Task<String> GetAllCurrencies()
    {
        using (HttpClient client = new HttpClient())
        {
            var newApiUrl = Config.BaseURL+Config.endOftheUrl;
            HttpResponseMessage response = await client.GetAsync(newApiUrl);
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }

    }
    [HttpGet("Exchange")]
    public async Task<string> GetFromCurrencies([FromQuery] string fromCurrency)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                var apiUrl = $"{Config.BaseURL}/{Uri.EscapeDataString(fromCurrency.ToLower())}{Config.endOftheUrl}";

                Console.WriteLine(apiUrl);

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP isteği sırasında hata oluştu: {ex.Message}");
            return "Hata oluştu";
        }
    }



    [HttpGet("Exchange/{sourceCurrency}/{targetCurrency}")]
    public async Task<string> GetExchangeRates([FromRoute] string sourceCurrency, [FromRoute] string targetCurrency)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                var apiUrl = $"{Config.BaseURL}/{Uri.EscapeDataString(sourceCurrency.ToLower())}/{Uri.EscapeDataString(targetCurrency.ToLower())}{Config.endOftheUrl}";
                    
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP isteği sırasında hata oluştu: {ex.Message}");
            return "Hata oluştu";
        }
    }

    [HttpGet("Convert/{sourceCurrency}/{targetCurrency}")]
    public async Task<decimal> ConvertCurrency(string sourceCurrency, string targetCurrency, decimal value)
    {
        sourceCurrency = sourceCurrency.ToLower();
        targetCurrency = targetCurrency.ToLower();
        var newApiUrl = Config.BaseURL + "/" + $"{sourceCurrency}/{targetCurrency}.json";

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(newApiUrl);
            string content = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(content);
            json.TryGetValue(targetCurrency, out var currencyRate);
            decimal convertedValue = value * ((decimal)currencyRate);
            return convertedValue;
        }
    }




 


}