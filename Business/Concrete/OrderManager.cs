using Business.Abstract;
using Business.Services;
using Business.Utilities.Consts;
using Business.Utilities.Results;
using Business.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Identity;
using Entities.Dtos.Order;

namespace Business.Concrete;

public class OrderManager : IOrderService
{
    private readonly IOrderDal _orderDal;
    private readonly ICacheService _cacheService;
    private readonly IProductService _productService;
    private readonly IUserService _userService;

    public OrderManager(IOrderDal orderDal, IProductService productService, IUserService userService)
    {
        _orderDal = orderDal;
        _productService = productService;
        _userService = userService;
    }

    public async Task<IDataResult<PlaceOrderResponse>> PlaceOrder(PlaceOrderRequest placeOrderRequest)
    {
        int userId = placeOrderRequest.UserId;

        bool isEmployee = await CheckIfEmployee(userId);
        decimal totalAmount = await CalculateTotalAmount(placeOrderRequest.OrderDetails);
        decimal discountAmount = await ApplyDiscount(isEmployee, userId, totalAmount);

        PlaceOrderResponse response = new()
        {
            OriginalPrice = totalAmount,
            DiscountAmount = discountAmount,
            FinalPrice = totalAmount - discountAmount
        };

        Order addedOrder = new()
        {
            UserId = userId,
            PurchaseDate = DateTime.Now,
            DiscountAmount = discountAmount,
            TotalAmount = totalAmount - discountAmount
        };
        await _orderDal.AddAsync(addedOrder);

        foreach (var detail in placeOrderRequest.OrderDetails)
        {
            var result = await _productService.GetByIdAsync(detail.ProductId);
            var product = result.Data;
            OrderDetail addedOrderDetail = new()
            {
                OrderId = addedOrder.Id,
                ProductId = product.Id,
                UnitPrice = product.Price,
                Quantity = detail.Quantity,
            };

            await _orderDal.AddOrderDetailAsync(addedOrderDetail);
        }

        return new SuccessDataResult<PlaceOrderResponse>(response, ResultMessages.Success.OrderPlace);
    }

    private async Task<decimal> ApplyDiscount(bool isEmployee, int userId, decimal totalAmount)
    {
        if (isEmployee)
        {
            return totalAmount * .3m;
        }
        else
        {
            return await IsPremiumCustomer(userId) ? totalAmount * .1m : 0;
        }
    }

    private async Task<bool> IsPremiumCustomer(int userId)
    {
        DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);
        decimal totalOrderAmount = await _orderDal.GetTotalOrderAmountByUserId(userId, oneMonthAgo);
        return totalOrderAmount >= 100;
    }

    private async Task<decimal> CalculateTotalAmount(List<OrderDetailDto> orderDetails)
    {
        decimal totalAmount = 0;

        foreach (var detail in orderDetails)
        {
            var result = await _productService.GetByIdAsync(detail.ProductId);
            var product = result.Data;
            totalAmount += product.Price * detail.Quantity;
        };

        return totalAmount;
    }

    private async Task<bool> CheckIfEmployee(int userId)
    {

        List<AppRole> userRoles = await _userService.GetUserRoleByIdAsync(userId);
        return userRoles.Any(role => role.Name == "employee");
    }
}
