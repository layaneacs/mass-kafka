using Microsoft.Extensions.Hosting;

namespace Consumer;
public class Worker : BackgroundService
{
    public override async Task StartAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Running");
        await base.StartAsync(stoppingToken);
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}
