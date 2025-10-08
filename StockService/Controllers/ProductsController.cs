using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockService.Data;
using StockService.Models;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly StockDbContext _db;

    public ProductsController(StockDbContext db) => _db = db;

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(Product input)
    {
        _db.Products.Add(input);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll() => Ok(await _db.Products.ToListAsync());

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var p = await _db.Products.FindAsync(id);
        if (p == null) return NotFound();
        return Ok(p);
    }

    // Endpoint para ajuste manual de estoque
    [HttpPut("{id}/quantity")]
    [Authorize]
    public async Task<IActionResult> UpdateQuantity(Guid id, [FromBody] int quantity)
    {
        var p = await _db.Products.FindAsync(id);
        if (p == null) return NotFound();
        p.Quantity = quantity;
        await _db.SaveChangesAsync();
        return NoContent();
    }
}