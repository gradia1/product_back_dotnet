using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Data;
using MyApiProject.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductWithStock()
        {
            var usersWithOrders = await _context.Product
                .Include(p => p.Stock)
                .ToListAsync();

            return Ok(usersWithOrders);
        }
        //    public async Task<ActionResult<IEnumerable<Product>>> GetUsers()
        //     {
        //         // return await _context.Product.Include(p => p.Stock)
        //         // .ToListAsync();
        //         var product_stock = await _context.Product.Include(p=>p.Stock).ToListAsync();

        //         return product_stock;
        //     }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductId(string id)
        {
            var product = await _context.Product
                .Include(p => p.Stock) // รวมข้อมูล Profile
                .FirstOrDefaultAsync(u => u.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("stock")]
        public async Task<ActionResult<Stock>> GetStock()
        {
            var product = await _context.Stock.ToListAsync();


            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


    }


}
