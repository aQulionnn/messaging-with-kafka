using Contracts;
using MassTransit;

namespace Consumer.Api.Handlers;

public class PlaceOrderHandler(ILogger<PlaceOrderHandler> logger)
    : IConsumer<PlaceOrder>
{
    public async Task Consume(ConsumeContext<PlaceOrder> context)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        
        logger.LogError("Order with ID {OrderId} placed by {CustomerName} for a total of {Sum}.", 
            context.Message.OrderId, context.Message.CustomerName,  context.Message.Sum);
    }
}