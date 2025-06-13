using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockAPI.Data;
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

        // GET: api/boxes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Box>>> GetBoxes()
        {
            return await _context.Boxes
                .Include(b => b.Transactions) // Incluye las transacciones de cada caja
                .ToListAsync();
        }

        // GET: api/boxes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Box>> GetBox(int id)
        {
            var box = await _context.Boxes
                .Include(b => b.Transactions)
                    .ThenInclude(t => t.Product)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (box == null)
            {
                return NotFound();
            }

            return box;
        }
    }
}
