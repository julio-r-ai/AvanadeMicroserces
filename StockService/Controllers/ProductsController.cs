using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockService.Data;
using EntitiesProduct = StockService.Data.Entities.Product; // alias para a entidade
using ModelsProduct = StockService.Models.Product;          // alias para o DTO

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly StockDbContext _db;

    public ProductsController(StockDbContext db) => _db = db;

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(ModelsProduct input) // usa o DTO
    {
        // converte DTO em entidade
        var entity = new EntitiesProduct
        {
            Id = Guid.NewGuid(),
            Name = input.Name,
            Price = input.Price,
            Quantity = input.Quantity
        };

        _db.Products.Add(entity);
        await _db.SaveChangesAsync();

        // retorna o DTO com Id gerado
        input.Id = entity.Id;
        return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        // converte entidades para DTOs
        var products = await _db.Products
            .Select(p => new ModelsProduct
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity
            }).ToListAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var p = await _db.Products.FindAsync(id);
        if (p == null) return NotFound();

        var dto = new ModelsProduct
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Quantity = p.Quantity
        };

        return Ok(dto);
    }

    [HttpPut("{id}/quantity")]
    [Authorize]
    public async Task<IActionResult> UpdateQuantity(Guid id, [FromBody] int quantity)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null) return NotFound();

        product.Quantity = quantity;
        await _db.SaveChangesAsync();

        return NoContent();
    }
}