using CurrencyApplication.ApplicationConfig;
using CurrencyApplication.Entity;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CurrencyController : ControllerBase
{

    private const string BaseUrl = "https://currency-exchange.p.rapidapi.com";

    [HttpGet]
    public async Task<IActionResult> GetExchange(string fromCurrency)
    {
        string body="";
        var client = new HttpClient();
        var request = new HttpRequestMessage
            
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{Config.BaseURL}/latest?from={fromCurrency}"),
            Headers =
            {
                { "X-RapidAPI-Key", $"{Config.APIKEY}" },
                { "X-RapidAPI-Host", "currency-conversion-and-exchange-rates.p.rapidapi.com" },
            },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);
        }

        return Ok(body);
    }
    [HttpGet("fromto")]
    public void GetExchangeRates([FromQuery] CurrencyRequestModel currencyRequestModel)
    {
        
        /* Console.WriteLine(FinalEndPoint);
         var client = new HttpClient() ;
         var response = await client.GetAsync(FinalEndPoint);
         var content = await response.Content.ReadAsStringAsync();
         return Ok(content);*/
    }


 


}