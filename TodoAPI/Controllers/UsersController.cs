using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Net;
using System.Text.RegularExpressions;
using TodoAPI.DB_Contexts;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        //GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            return await _context.Users.ToListAsync();
        }

        //GET: api/Users/2
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
                var user = await _context.Users.FindAsync(id);

                if (user == null) {
                    return this.StatusCode(StatusCodes.Status400BadRequest, new { message = "User not found with ID " + id });
                }

                return user;
        }

        //post: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if(_context.Users == null)
            {
                return Problem("The data base does not contain user table.");
            }

            //if (String.IsNullOrEmpty(user.FullName) || String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.Password))
            //{
            //    return this.StatusCode(StatusCodes.Status400BadRequest, new { error = "Please enter all fields" });
            //}

            //if (!Regex.IsMatch(user.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            //{
            //    return this.StatusCode(StatusCodes.Status400BadRequest, new { error = "Please enter an valid email" });
            //}
                
            if(user.Password.Length < 8)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, new { error = "The password must be 8 character long." });
            }

            User existingUser = await _context.Users.SingleOrDefaultAsync(x => x.Email == user.Email);

            if (existingUser != null)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, new { error = "Email address already in use." });
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }
    }
}
