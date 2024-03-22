using AutoMapper;
using Business.Abstract;
using Entities.Dtos.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
        }


        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest placeOrderRequest)
        {
            var result = await _orderService.PlaceOrder(placeOrderRequest);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
