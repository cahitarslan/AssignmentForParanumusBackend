using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Repositories;

public class EfOrderDal : EfEntityRepositoryBase<Order, BaseDbContext>, IOrderDal
{
    private readonly BaseDbContext _context;

    public EfOrderDal(BaseDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task AddOrderDetailAsync(OrderDetail orderDetail)
    {
        var addedOrderDetail = Context.Entry(orderDetail);
        addedOrderDetail.State = EntityState.Added;
        await _context.SaveChangesAsync();
    }

    public async Task<decimal> GetTotalOrderAmountByUserId(int userId, DateTime startDate)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId && o.PurchaseDate >= startDate)
            .SumAsync(o => o.TotalAmount);
    }
}
