using System;
using System.Timers;
using Timer = System.Timers.Timer;
using Tourist.ShipmentMobile.Infrastructure;
namespace Tourist.ShipmentMobile.Jobs;
public class SyncDataJob
{
    Timer _timer;
    readonly ShipmentsDatabase _dataRepository;
    public SyncDataJob(ShipmentsDatabase dataRepository)
    {
        _dataRepository = dataRepository;
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
        _timer = new Timer(10000);
        _timer.Elapsed += new ElapsedEventHandler(async (s, e) => await TimerTick(s, e));
        _timer.Start();
    }

    private async Task DoSync()
    {
        // TO DO: Update items
        var itemsToSync = _dataRepository.GetDirtyItems();

        // TO DO: Update customers
        var customersToSync = _dataRepository.GetDirtyCutomers();

        // TO DO: Update shipments
        var shipmentsToSync = _dataRepository.GetDirtyShipmentAsync();
    }
}
