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
    public class StockController : ControllerBase
    {
        //private readonly AppDbContext _context;
        private readonly MySqlService _mySqlService;

        public StockController(MySqlService mySqlService)
        {
            _mySqlService = mySqlService;
        }



        [HttpGet]
        public async Task<IActionResult> GetStocksAsync(int id)
        {
            var name = await _mySqlService.GetStocksAsync();

            if (name == null)
            {
                return NotFound();
            }
            Console.WriteLine(name);

            return Ok(name);
        }

        [HttpPost]

        public async Task<ActionResult> CutStock([FromBody] List<StockDto> stocks)
        {
            //Console.WriteLine(stocks.List);
            foreach (var stock in stocks)
             {
                 await _mySqlService.UpdateStockAsync(stock.Id , stock.Amount);
            }

            return NoContent();


        }


    }


}
