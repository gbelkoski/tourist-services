using System;
using System.Timers;
using Timer = System.Timers.Timer;
using Tourist.ShipmentMobile.Infrastructure;
namespace Tourist.ShipmentMobile.Jobs;
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
        await SyncItems();
        await SyncCustomers();
        await SyncShipments();
    }

    private async Task SyncItems()
    {
        var serverItems = await _touristApiClient.GetItems();
        var localItems = await _dataRepository.GetItemsAsync();

        await _dataRepository.DeleteItemAsync(localItems.FirstOrDefault(a=>a.Code == null));
        localItems.RemoveAll(a => a.Code == null);

        var missingItems = serverItems.Where(s => !localItems.Any(l => l.Code == s.Code));
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

        var missingCustomers = serverCustomers.Where(s => !localCustomers.Any(l => l.Code == s.Code));
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
