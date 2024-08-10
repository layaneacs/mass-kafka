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
public class OrderRetryConsumer : IConsumer<OrderMessage>
{
    public Task Consume(ConsumeContext<OrderMessage> context)
    {
        if (context.GetRetryAttempt() == 0)
        {
            Console.WriteLine($"Contrato recebido pela primeira vez: {@context.Message.ContractId} - {DateTime.UtcNow}");
            throw new OrderRetryException();
        }
        if (context.GetRetryAttempt() == 1)
        {
            Console.WriteLine($"Contrato recebido pela segunda vez: {@context.Message.ContractId} - {DateTime.UtcNow}");
            throw new OrderRetryDelayException();
        }

        Console.WriteLine($"Contrato aceito: {@context.Message.ContractId} - {DateTime.UtcNow}");

        return Task.CompletedTask;
    }
}


public class OrderRetryException : Exception
{

}
public class OrderRetryDelayException : Exception
{

}
