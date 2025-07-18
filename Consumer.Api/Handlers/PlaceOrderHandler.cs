using Consumer.Api.Collections;
using Consumer.Api.Repositories;
using Contracts;
using MassTransit;

namespace Consumer.Api.Handlers;

public class PlaceOrderHandler(ILogger<PlaceOrderHandler> logger, IOrderRepository orderRepository)
    : IConsumer<PlaceOrder>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly ILogger<PlaceOrderHandler> _logger = logger;
    
    public async Task Consume(ConsumeContext<PlaceOrder> context)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));

        var order = new Order
        {
            Id = Guid.NewGuid().ToString(),
            CustomerName = context.Message.CustomerName,
            Sum = context.Message.Sum
        };
        
        await _orderRepository.InsertAsync(order);
        
        _logger.LogInformation("Order with ID {OrderId} placed by {CustomerName} for a total of {Sum}.", 
            order.Id, order.CustomerName, order.Sum);
    }
}