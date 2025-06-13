using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockAPI.Data;
using StockAPI.Dtos;
using StockAPI.Models;

namespace StockAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoxesController : ControllerBase
    {
        private readonly DataContext _context;

        public BoxesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoxDTO>>> GetBoxes()
        {
            var boxes = await _context.Boxes
                .Include(b => b.Transactions)
                    .ThenInclude(t => t.Product)
                .ToListAsync();

            var boxDtos = boxes.Select(b => new BoxDTO
            {
                Id = b.Id,
                Code = b.Code,
                Quantity = b.Quantity,
                Location = b.Location,
                Transactions = b.Transactions.Select(t => new Dtos.TransactionDTO
                {
                    Id = t.Id,
                    Date = t.Date,
                    ProductId = t.ProductId,
                    ProductName = t.Product.Name
                }).ToList()
            }).ToList();

            return Ok(boxDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BoxDTO>> GetBox(int id)
        {
            var box = await _context.Boxes
                .Include(b => b.Transactions)
                    .ThenInclude(t => t.Product)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (box == null)
            {
                return NotFound();
            }

            var boxDto = new BoxDTO
            {
                Id = box.Id,
                Code = box.Code,
                Quantity = box.Quantity,
                Location = box.Location,
                Transactions = box.Transactions.Select(t => new Dtos.TransactionDTO
                {
                    Id = t.Id,
                    Date = t.Date,
                    ProductId = t.ProductId,
                    ProductName = t.Product.Name
                }).ToList()
            };

            return Ok(boxDto);
        }
    }
}
