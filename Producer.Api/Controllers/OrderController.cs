using Bogus;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Producer.Api.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController(ITopicProducer<PlaceOrder> producer) : ControllerBase
{
    private readonly ITopicProducer<PlaceOrder> _producer = producer;
    
    [HttpPost("place")]
    public async Task<IActionResult> PlaceOrders()
    {
        for (int i = 0; i < 10; i++)
        {
            var orderPlacedFaker = new Faker<PlaceOrder>()
                .CustomInstantiator(f => new PlaceOrder(
                    Guid.NewGuid(),
                    f.Name.FullName(),
                    (double)f.Finance.Amount()
                ));
            
            await _producer.Produce(orderPlacedFaker.Generate());
        }
        
        return Accepted();
    }
}