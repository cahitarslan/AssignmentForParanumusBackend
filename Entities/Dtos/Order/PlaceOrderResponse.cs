namespace Entities.Dtos.Order;

public class PlaceOrderResponse
{
    public decimal OriginalPrice { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal FinalPrice { get; set; }
}
