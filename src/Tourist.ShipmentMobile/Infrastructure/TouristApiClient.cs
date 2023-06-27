using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tourist.Domain;

namespace Tourist.ShipmentMobile.Infrastructure;
public class TouristApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string Username = "Cleanex";
    private readonly string Password = "CleanX0099";

    public TouristApiClient()
    {
        _httpClient = new HttpClient();

        _httpClient.BaseAddress = new Uri(Constants.TouristApi);
        var authenticationString = $"{Username}:{Password}";
        var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(authenticationString));
        _httpClient.DefaultRequestHeaders.Add($"Authorization", $"Basic {base64EncodedAuthenticationString}");
    }

    public async Task<List<Customer>> GetCustomers()
    {
        using var httpResponseMessage =
            await _httpClient.GetAsync("/customers");

        var result = await httpResponseMessage.Content.ReadAsStringAsync();

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<List<Customer>>(result, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        return new List<Customer>();
    }

    public async Task<bool> PostSyncCustomers(List<Customer> customers)
    {
        var customersJson = new StringContent(
            JsonSerializer.Serialize(new { Customers = customers }),
            Encoding.UTF8,
            System.Net.Mime.MediaTypeNames.Application.Json);

        using var httpResponseMessage =
            await _httpClient.PostAsync("/sync/customers", customersJson);
        
        return httpResponseMessage.IsSuccessStatusCode;
    }

    public async Task<List<Item>> GetItems()
    {
        using var httpResponseMessage =
            await _httpClient.GetAsync("/items");

        var result = await httpResponseMessage.Content.ReadAsStringAsync();

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<List<Item>>(result, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        return new List<Item>();
    }

    public async Task<bool> PostSyncItems(List<Item> items)
    {
        var itemsJson = new StringContent(
        JsonSerializer.Serialize(new { Items = items }),
        Encoding.UTF8,
        System.Net.Mime.MediaTypeNames.Application.Json);

        using var httpResponseMessage =
            await _httpClient.PostAsync("/sync/items", itemsJson);

        return httpResponseMessage.IsSuccessStatusCode;
    }

    public async Task<bool> PostSyncShipments(List<ShipmentLineItem> shipments)
    {
        var shipmentsJson = new StringContent(
        JsonSerializer.Serialize(new { Shipments = shipments }),
        Encoding.UTF8,
        System.Net.Mime.MediaTypeNames.Application.Json);

        using var httpResponseMessage =
            await _httpClient.PostAsync("/sync/shipments", shipmentsJson);

        return httpResponseMessage.IsSuccessStatusCode;
    }

    public async Task<bool> PostSync(List<object> entities, string entityName)
    {
        var entitiesJson = new StringContent(
        JsonSerializer.Serialize(new { entityName = entities }),
        Encoding.UTF8,
        System.Net.Mime.MediaTypeNames.Application.Json);

        using var httpResponseMessage =
            await _httpClient.PostAsync($"/sync/Sync{entityName}", entitiesJson);

        return httpResponseMessage.IsSuccessStatusCode;
    }
}
