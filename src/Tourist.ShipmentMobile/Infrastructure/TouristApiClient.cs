using System;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Tourist.Domain;

namespace Tourist.ShipmentMobile.Infrastructure;
public class TouristApiClient
{
    private readonly HttpClient _httpClient;

    public TouristApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("https://localhost:7218");
    }

    public async Task<bool> PostSyncCustomers(List<Customer> customers)
    {
        var customersJson = new StringContent(
        JsonSerializer.Serialize(customers),
        Encoding.UTF8,
        System.Net.Mime.MediaTypeNames.Application.Json); // using static System.Net.Mime.MediaTypeNames;

        using var httpResponseMessage =
            await _httpClient.PostAsync("/sync/customers", customersJson);
        
        httpResponseMessage.EnsureSuccessStatusCode();
        return httpResponseMessage.IsSuccessStatusCode;
    }
}
