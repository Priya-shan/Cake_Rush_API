using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cake_Rush_API.Data;
using Cake_Rush_API.Models;
using Cake_Rush_API.Dao;

namespace Cake_Rush_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Cake_Rush_APIContext _context;

        public UserController(Cake_Rush_APIContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUserModel()
        {
          if (_context.UserModel == null)
          {
              return NotFound();
          }
            return await _context.UserModel.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserModel(string id)
        {
          if (_context.UserModel == null)
          {
              return NotFound();
          }
            var userModel = await _context.UserModel.FindAsync(id);

            if (userModel == null)
            {
                return NotFound();
            }

            return userModel;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserModel(string id, DaoUser user)
        {
            //if (id != user.userId)
            //{
            //    return BadRequest();
            //}
            UserModel userModel = _context.UserModel.FirstOrDefault(u => u.userId == id);
            if(userModel != null) {
                Console.WriteLine("model found");
                userModel.userName = user.userName;
                userModel.mobile = user.mobile;
                userModel.address = user.address;
                userModel.city = user.city;
                userModel.pincode = user.pincode;
                _context.Entry(userModel).State = EntityState.Modified;
            }
            else
            {
                Console.WriteLine("model not found");
                return BadRequest();
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserModelExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserModel>> PostUserModel([FromBody] DaoUser user)
        {
          if (_context.UserModel == null)
          {
              return Problem("Entity set 'Cake_Rush_APIContext.UserModel'  is null.");
          }
            UserModel userModel = new UserModel()
            {
                userId = user.userId,
                userName=user.userName,
                email=user.email,
                mobile=user.mobile,
                address=user.address,
                city=user.city,
                pincode=user.pincode
            };
            _context.UserModel.Add(userModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserModelExists(userModel.userId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserModel", new { id = userModel.userId }, userModel);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserModel(string id)
        {
            if (_context.UserModel == null)
            {
                return NotFound();
            }
            var userModel = await _context.UserModel.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }

            _context.UserModel.Remove(userModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserModelExists(string id)
        {
            return (_context.UserModel?.Any(e => e.userId == id)).GetValueOrDefault();
        }
    }
}
