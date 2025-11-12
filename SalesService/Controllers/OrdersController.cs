using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesService.Data;
using SalesService.Models;
using SalesService.Messaging;
using SalesService.DTOs;



[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly SalesContext _db;
    private readonly IPublishEndpoint _publish;

    public OrdersController(SalesContext db, IPublishEndpoint publish)
    {
        _db = db;
        _publish = publish;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] Order order)
    {
        order.Status = "Pending";
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        var eventMsg = new OrderCreatedEvent(order.Id,
            order.Items.Select(i => new OrderItemDto(i.ProductId, i.Quantity)).ToList(),
            order.CustomerName, order.CreatedAt);

        // Publica evento para o StockService processar
        await _publish.Publish(eventMsg);

        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll() => Ok(await _db.Orders.Include(o => o.Items).ToListAsync());

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var o = await _db.Orders.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);
        if (o == null) return NotFound();
        return Ok(o);
    }

}