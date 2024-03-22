using Business.Utilities.Results.Abstract;
using Entities.Dtos.Order;

namespace Business.Abstract;

public interface IOrderService
{
    Task<IDataResult<PlaceOrderResponse>> PlaceOrder(PlaceOrderRequest placeOrderRequest);
}
