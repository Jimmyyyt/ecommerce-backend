using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApi;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SqlContext _context;

        public AuthenticationController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Authentication
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Authentication/5
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

        // PUT: api/Authentication/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
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

        // POST: api/Authentication
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("SignIn")]
        public async Task<ActionResult<dynamic>> SignIn(SignInModel model)
        {

            var user = await _context.Users.Where(x => x.Email == model.Email).FirstOrDefaultAsync();

            if(user != null)
            {
                if(user.Password == model.Password)
                {
                    return new OkObjectResult(JsonConvert.SerializeObject(new { userId = user.Id, sessionId = Guid.NewGuid().ToString() }));
                }

                return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = "incorrect email or password" }));
            }

            return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = "incorrect email or password" }));
        }

        [HttpPost("SignUp")]
        public async Task<ActionResult<dynamic>> SignUp(SignUpModel model)
        {

            var user = await _context.Users.Where(x => x.Email == model.Email).FirstOrDefaultAsync();

            if (user == null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return new OkResult();
            }

            return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = "a user with the same email address already exists" }));
        }

        // DELETE: api/Authentication/5
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
