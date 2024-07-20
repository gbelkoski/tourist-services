using System;
using System.Timers;
using Timer = System.Timers.Timer;
using Cleanex.ClientApp.Infrastructure;
using Microsoft.AppCenter.Crashes;

namespace Cleanex.ClientApp.Jobs;
public class SyncDataJob
{
    Timer _timer;
    readonly ShipmentsDatabase _dataRepository;
    readonly TouristApiClient _touristApiClient;

    public SyncDataJob(ShipmentsDatabase dataRepository, TouristApiClient apiClient)
    {
        _dataRepository = dataRepository;
        _touristApiClient = apiClient;
    }

    public void Schedule()
    {
        ScheduleTimer();
    }

    private async Task TimerTick(object sender, ElapsedEventArgs e)
    {
        _timer.Stop();
        await DoSync();
        ScheduleTimer();
    }

    private void ScheduleTimer()
    {
        DateTime nowTime = DateTime.Now;
        _timer = new Timer(30000);
        _timer.Elapsed += new ElapsedEventHandler(async (s, e) => await TimerTick(s, e));
        _timer.Start();
    }

    private async Task DoSync()
    {
        try
        {
            await SyncItems();
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }

        try
        {
            await SyncCustomers();
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }

        try
        {
            await SyncShipments();
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }
    }

    private async Task SyncItems()
    {
        var serverItems = await _touristApiClient.GetItems();
        var localItems = await _dataRepository.GetItemsAsync();

        var missingItems = serverItems.Where(s => !localItems.Any(l => l.Id == s.Id));
        foreach( var item in missingItems )
        {
            await _dataRepository.SaveItemAsync(item);
        }

        var itemsToSync = await _dataRepository.GetDirtyItems();
        if (itemsToSync.Any())
        {
            var success = await _touristApiClient.PostSyncItems(itemsToSync);
            if (success)
            {
                itemsToSync.ForEach(async i =>
                {
                    await _dataRepository.MarkEntityNotDirty(i);
                });
            }
        }
    }

    private async Task SyncCustomers()
    {
        var serverCustomers = await _touristApiClient.GetCustomers();
        var localCustomers = await _dataRepository.GetCustomersAsync();

        await _dataRepository.DeleteCustomerAsync(localCustomers.FirstOrDefault(a => a.Code == null));
        localCustomers.RemoveAll(a => a.Code == null);

        var missingCustomers = serverCustomers.Where(s => !localCustomers.Any(l => l.Id == s.Id));
        foreach (var customer in missingCustomers)
        {
            await _dataRepository.SaveCustomerAsync(customer);
        }

        var customersToSync = await _dataRepository.GetDirtyCutomers();
        if (customersToSync.Any())
        {
            bool success = await _touristApiClient.PostSyncCustomers(customersToSync);
            if (success)
            {
                customersToSync.ForEach(async c =>
                {
                    await _dataRepository.MarkEntityNotDirty(c);
                });
            }
        }
    }

    private async Task SyncShipments()
    {
        var shipmentsToSync = await _dataRepository.GetDirtyShipmentAsync();
        if (shipmentsToSync.Any())
        {
            var success = await _touristApiClient.PostSyncShipments(shipmentsToSync);
            if (success)
            {
                shipmentsToSync.ForEach(async s =>
                {
                    await _dataRepository.MarkEntityNotDirty(s);
                });
            }
        }
    }
}
