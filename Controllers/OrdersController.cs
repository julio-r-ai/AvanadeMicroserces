using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesService.Data;
using SalesService.Models;

namespace SalesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly SalesContext _context;

        public OrdersController(SalesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders() => Ok(await _context.Orders.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrders), order);
        }
    }
}