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
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        //public async Task<ActionResult<IEnumerable<Login>>> GetUsers()
        {
            return await _context.Users.OrderBy(u=>u.Id).ToListAsync(); 
            
            //return await _context.Login.ToListAsync();      
        }

        [HttpGet("login")]
        public async Task<ActionResult<IEnumerable<Login>>> GetLogin()
        {
            return await _context.Login.ToListAsync();
        }

        [HttpGet("login/{id}")]
        public async Task<ActionResult<Login>> GetLogin(int id)
        {
            var logi =  await _context.Login.FindAsync(id);
            if (logi == null)
            {
                return NotFound();
            }

            return logi;
        }

 
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
