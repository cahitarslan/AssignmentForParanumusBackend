namespace Entities.Dtos.Order;

public class PlaceOrderRequest
{
    public int UserId { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; }
}
