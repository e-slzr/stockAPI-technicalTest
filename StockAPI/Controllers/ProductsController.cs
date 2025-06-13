using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockAPI.Data;
using StockAPI.Models;
using StockAPI.Dtos;
using System.Linq;

namespace StockAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        // Obtiene todos los productos con sus transacciones y cajas relacionadas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products
                .Include(p => p.Transactions)
                    .ThenInclude(t => t.Box)
                .ToListAsync();
        }

        // Obtiene un producto por su Id, con transacciones y cajas relacionadas
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Transactions)
                    .ThenInclude(t => t.Box)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // Nuevo endpoint para obtener stock disponible por producto
        [HttpGet("stock-available")]
        public async Task<ActionResult<IEnumerable<ProductStockDto>>> GetAvailableStock()
        {
            var products = await _context.Products
                .Include(p => p.Transactions)
                .ToListAsync();

            var result = products.Select(p =>
            {
                var lastTransaction = p.Transactions
                    .OrderByDescending(t => t.Date)
                    .FirstOrDefault();

                var availableStock = p.Transactions
                    .Where(t => t.OperationType == "IN").Sum(t => t.Quantity) -
                    p.Transactions.Where(t => t.OperationType == "OUT").Sum(t => t.Quantity);

                return new ProductStockDto
                {
                    ProductId = p.Id,
                    Code = p.Code,
                    Name = p.Name,
                    Description = p.Description,
                    Status = p.Status,
                    AvailableStock = availableStock,
                    LastTransactionDate = lastTransaction?.Date,
                    LastOperationType = lastTransaction?.OperationType,
                    LastTransactionQuantity = lastTransaction?.Quantity
                };
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<ProductDetailDto>> GetProductDetails(int id)
        {
            var product = await _context.Products
                .Include(p => p.Transactions)
                    .ThenInclude(t => t.Box)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            var boxGroups = product.Transactions
                .Where(t => t.OperationType == "IN")
                .GroupBy(t => t.BoxId)
                .Select(g =>
                {
                    var lastTransaction = g.OrderByDescending(t => t.Date).FirstOrDefault();
                    var totalQuantity = g.Sum(t => t.Quantity) -
                        product.Transactions
                            .Where(t => t.BoxId == g.Key && t.OperationType == "OUT")
                            .Sum(t => t.Quantity);

                    return new BoxDetailDto
                    {
                        BoxId = g.Key,
                        BoxCode = lastTransaction?.Box?.Code ?? "",
                        Quantity = totalQuantity,
                        LastTransactionDate = lastTransaction?.Date
                    };
                })
                .Where(b => b.Quantity > 0)
                .ToList();

            var result = new ProductDetailDto
            {
                ProductId = product.Id,
                Code = product.Code,
                Name = product.Name,
                Description = product.Description,
                Status = product.Status,
                Boxes = boxGroups
            };

            return Ok(result);
        }

    }
}
