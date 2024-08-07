using Consumer.Contracts;
using MassTransit;

namespace Consumer;

public class OrderConsumer : IConsumer<OrderMessage>
{
    public Task Consume(ConsumeContext<OrderMessage> context)
    {
        Console.WriteLine($"Contrato recebido: {@context.Message.ContractId}");

        return Task.CompletedTask;
    }
}
