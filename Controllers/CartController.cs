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
    public class CartController : ControllerBase
    {
        private readonly Cake_Rush_APIContext _context;

        public CartController(Cake_Rush_APIContext context)
        {
            _context = context;
        }

        // GET: api/Cart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartModel>>> GetCartModel()
        {
          if (_context.CartModel == null)
          {
              return NotFound();
          }
            var carts = await _context.CartModel.Include(u => u.User).Include(o => o.SubCatMap).ThenInclude(s=>s.Product).ToListAsync();
            foreach(var item in carts)
            {
                if (item.User == null)
                {
                    item.User = await _context.UserModel.FindAsync(item.userId);
                }
                if(item.SubCatMap == null)
                {
                    item.SubCatMap = await _context.SubCategoryMapModel.FindAsync(item.mapId);
                }
                if(item.SubCatMap.Product == null)
                {
                    item.SubCatMap.Product = await _context.ProductModel.FindAsync(item.SubCatMap.productId);
                }
            }
            return Ok(carts);
            return await _context.CartModel.ToListAsync();
        }

        // GET: api/Cart/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartModel>> GetCartModel(int id)
        {
          if (_context.CartModel == null)
          {
              return NotFound();
          }
            var cartModel = await _context.CartModel.FindAsync(id);

            if (cartModel == null)
            {
                return NotFound();
            }

            return cartModel;
        }

        // PUT: api/Cart/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartModel(int id, DaoCart cart)
        {
            //if (id != cartModel.cartId)
            //{
            //    return BadRequest();
            //}

            CartModel cartModel = _context.CartModel.FirstOrDefault(u => u.cartId == id);
            if (cartModel != null) {
                Console.WriteLine("model found");
                cartModel.userId = cart.userId;
                cartModel.mapId = cart.mapId;
                cartModel.quantity = cart.quantity;
                cartModel.price = cart.price;
                cartModel.expiry = cart.expiry;
            }
            else
            {
                Console.WriteLine("model not found");
            }
            _context.Entry(cartModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartModelExists(id))
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

        // POST: api/Cart
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CartModel>> PostCartModel([FromBody] DaoCart cart)
        {
          if (_context.CartModel == null)
          {
              return Problem("Entity set 'Cake_Rush_APIContext.CartModel'  is null.");
          }
          CartModel cartModel =new CartModel()
          {
              userId=cart.userId,
              mapId=cart.mapId,
              quantity=cart.quantity,
              price=cart.price
          };
            _context.CartModel.Add(cartModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCartModel", new { id = cartModel.cartId }, cartModel);
        }

        // DELETE: api/Cart/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartModel(int id)
        {
            if (_context.CartModel == null)
            {
                return NotFound();
            }
            var cartModel = await _context.CartModel.FindAsync(id);
            if (cartModel == null)
            {
                return NotFound();
            }

            _context.CartModel.Remove(cartModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartModelExists(int id)
        {
            return (_context.CartModel?.Any(e => e.cartId == id)).GetValueOrDefault();
        }
    }
}
