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
        private readonly SalesManager _salesManager;

        public SalesController(SalesManager salesManager)
        {
            _salesManager = salesManager;
        }

        // POST: api/sales
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSale([FromBody] Sale sale)
        {
            if (sale == null || !sale.Items.Any())
                return BadRequest("Venda inválida.");

            var result = await _salesManager.CreateSaleAsync(sale);
            return Ok(result);
        }

        // GET: api/sales
        [HttpGet]
        [Authorize]
        public IActionResult GetSales()
        {
            var sales = _salesManager.GetSales();
            return Ok(sales);
        }
    }
}