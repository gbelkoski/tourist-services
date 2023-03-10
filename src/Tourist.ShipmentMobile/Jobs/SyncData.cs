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
        }

        var customersToSync = await _dataRepository.GetDirtyCutomers();
        if(customersToSync.Any())
        {
            await _touristApiClient.PostSyncCustomers(customersToSync);
        }

        var shipmentsToSync = await _dataRepository.GetDirtyShipmentAsync();
        //if(shipmentsToSync.Any())
        //{
        //    await _touristApiClient.PostSyncShipments(shipmentsToSync);
        //}
    }
}
