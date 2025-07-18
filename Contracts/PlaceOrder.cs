namespace Contracts;

public record PlaceOrder(Guid MessageId, string CustomerName, double Sum);
