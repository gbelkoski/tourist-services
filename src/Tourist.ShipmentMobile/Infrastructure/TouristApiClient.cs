﻿using System;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Tourist.Domain;

namespace Tourist.ShipmentMobile.Infrastructure;
public class TouristApiClient
{
    private readonly HttpClient _httpClient;

    public TouristApiClient()
    {
        _httpClient = new HttpClient();

        _httpClient.BaseAddress = new Uri("http://192.168.86.20:5000");
    }

    public async Task<bool> PostSyncCustomers(List<Customer> customers)
    {
        var customersJson = new StringContent(
        JsonSerializer.Serialize(new { Customers = customers }),
        Encoding.UTF8,
        System.Net.Mime.MediaTypeNames.Application.Json); // using static System.Net.Mime.MediaTypeNames;

        var cus = await customersJson.ReadAsStringAsync();
        using var httpResponseMessage =
            await _httpClient.PostAsync("/sync/customers", customersJson);
        
        httpResponseMessage.EnsureSuccessStatusCode();
        return httpResponseMessage.IsSuccessStatusCode;
    }

    public async Task<bool> PostSyncItems(List<Item> items)
    {
        var itemsJson = new StringContent(
        JsonSerializer.Serialize(new { Items = items }),
        Encoding.UTF8,
        System.Net.Mime.MediaTypeNames.Application.Json); // using static System.Net.Mime.MediaTypeNames;

        using var httpResponseMessage =
            await _httpClient.PostAsync("/sync/items", itemsJson);

        httpResponseMessage.EnsureSuccessStatusCode();
        return httpResponseMessage.IsSuccessStatusCode;
    }

    public async Task<bool> PostSyncShipments(List<ShipmentLineItem> shipments)
    {
        var shipmentsJson = new StringContent(
        JsonSerializer.Serialize(new { Shipments = shipments }),
        Encoding.UTF8,
        System.Net.Mime.MediaTypeNames.Application.Json); // using static System.Net.Mime.MediaTypeNames;

        using var httpResponseMessage =
            await _httpClient.PostAsync("/sync/shipments", shipmentsJson);

        httpResponseMessage.EnsureSuccessStatusCode();
        return httpResponseMessage.IsSuccessStatusCode;
    }

    public async Task<bool> PostSync(List<object> entities, string entityName)
    {
        var entitiesJson = new StringContent(
        JsonSerializer.Serialize(new { entityName = entities }),
        Encoding.UTF8,
        System.Net.Mime.MediaTypeNames.Application.Json); // using static System.Net.Mime.MediaTypeNames;

        var cus = await entitiesJson.ReadAsStringAsync();
        using var httpResponseMessage =
            await _httpClient.PostAsync($"/sync/Sync{entityName}", entitiesJson);

        httpResponseMessage.EnsureSuccessStatusCode();
        return httpResponseMessage.IsSuccessStatusCode;
    }
}