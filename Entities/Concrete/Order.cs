using Entities.Abstract;
using Entities.Concrete.Identity;

namespace Entities.Concrete;

public class Order : IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; }

    public AppUser User { get; set; }
}
