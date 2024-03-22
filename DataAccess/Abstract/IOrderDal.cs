using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IOrderDal : IEntityRepositoryAsync<Order>
{
    Task AddOrderDetailAsync(OrderDetail orderDetail);
    Task<decimal> GetTotalOrderAmountByUserId(int userId, DateTime startDate);
}
