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
        var itemsToSync = await _dataRepository.GetDirtyItems();
        if (itemsToSync.Any())
        {
            await _touristApiClient.PostSyncItems(itemsToSync);
            itemsToSync.ForEach(async i =>
            {
                await _dataRepository.MarkEntityNotDirty(i);
            });
        }

        var customersToSync = await _dataRepository.GetDirtyCutomers();
        if(customersToSync.Any())
        {
            await _touristApiClient.PostSyncCustomers(customersToSync);
            customersToSync.ForEach(async c =>
            {
                await _dataRepository.MarkEntityNotDirty(c);
            });
        }

        var shipmentsToSync = await _dataRepository.GetDirtyShipmentAsync();
        if (shipmentsToSync.Any())
        {
            await _touristApiClient.PostSyncShipments(shipmentsToSync);
            shipmentsToSync.ForEach(async s =>
            {
                await _dataRepository.MarkEntityNotDirty(s);
            });
        }
    }
}
