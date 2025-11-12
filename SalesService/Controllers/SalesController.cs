using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesService.Models;
using SalesService.Services;

namespace SalesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly SalesManager _manager;

        public SalesController(SalesManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            var newOrder = await _manager.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrders), new { id = newOrder.Id }, newOrder);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _manager.GetAllOrdersAsync();
            return Ok(orders);
        }
    }
}