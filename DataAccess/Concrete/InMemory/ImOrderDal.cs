using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace DataAccess.Concrete.InMemory;

public class ImOrderDal : ImEntityRepositoryBase<Order>, IOrderDal
{
    public List<Order> _orders;
    public List<OrderDetail> _orderDetails;

    public ImOrderDal()
    {
        _orders = new();
        _orderDetails = new();
    }

    public async override Task AddAsync(Order order)
    {
        order.Id = _orders.Count + 1;
        _orders.Add(order);
    }

    public async override Task DeleteAsync(Order order)
    {
        var deletedOrder = _orders.SingleOrDefault(o => o.Id == order.Id);
        if (deletedOrder != null)
        {
            _orders.Remove(deletedOrder);
        }
        else
        {
            throw new Exception("No such order found");
        }
    }

    public async override Task UpdateAsync(Order order)
    {
        var updatedOrder = _orders.SingleOrDefault(o => o.Id == order.Id);
        if (updatedOrder != null)
        {
            updatedOrder.PurchaseDate = DateTime.Now;
            //...
        }
        else
        {
            throw new Exception("No such order found");
        }
    }


    public async override Task<Order> GetAsync(Expression<Func<Order, bool>> filter)
    {
        //return base.GetAsync(filter);
        return _orders.AsQueryable().Where(filter).SingleOrDefault();
    }

    public async override Task<List<Order>> GetAllAsync(Expression<Func<Order, bool>> filter = null)
    {
        //return base.GetAllAsync(filter);
        return filter == null ? _orders.ToList() : _orders.AsQueryable().Where(filter).ToList();
    }


    public async Task AddOrderDetailAsync(OrderDetail orderDetail)
    {
        orderDetail.Id = _orderDetails.Count + 1;
        _orderDetails.Add(orderDetail);
    }
    public async Task<decimal> GetTotalOrderAmountByUserId(int userId, DateTime startDate)
    {
        return _orders
            .Where(o => o.UserId == userId && o.PurchaseDate >= startDate)
            .Select(o => o.TotalAmount)
            .Sum();
    }
}
