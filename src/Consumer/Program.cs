using Microsoft.Extensions.Hosting;
using MassTransit;
using Consumer.Contracts;
using Consumer;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddMassTransit(m =>
                {

                    m.UsingInMemory();
                    m.AddRider(r =>
                    {
                        r.AddConsumer<OrderConsumer>();
                        r.UsingKafka((context, config) =>
                        {
                            config.Host("localhost:9094");
                            config.TopicEndpoint<OrderMessage>("topic-primary", "consumer-primary", e =>
                            {
                                e.ConfigureConsumer<OrderConsumer>(context);
                            });
                        });

                    });

                });

                services.AddHostedService<Worker>();

            })
            .Build();

        host.Run();
    }
}
