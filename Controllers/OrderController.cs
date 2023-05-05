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
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Cake_Rush_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly Cake_Rush_APIContext _context;

        public OrderController(Cake_Rush_APIContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrderModel()
        {

            //if (_context.OrderModel == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.OrderModel.ToListAsync();

            //var options = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve
            //};

            var orders = await _context.OrderModel.Include(o => o.Cart).ThenInclude(c=>c.SubCatMap).ThenInclude(s=>s.Product).ToListAsync();
            foreach (var item in orders)
            {

                if (item.Cart == null)
                {
                    item.Cart = await _context.CartModel.FindAsync(item.cartId);
                }
                if (item.Cart.User == null)
                {
                    item.Cart.User = await _context.UserModel.FindAsync(item.Cart.userId);
                }
                if (item.Cart.SubCatMap == null)
                {
                    item.Cart.SubCatMap = await _context.SubCategoryMapModel.FindAsync(item.Cart.mapId);
                }
                if (item.Cart.SubCatMap.Product == null)
                {
                    item.Cart.SubCatMap.Product = await _context.ProductModel.FindAsync(item.Cart.SubCatMap.productId);
                }
            }
            return Ok(orders);
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderModel>> GetOrderModel(int id)
        {
          if (_context.OrderModel == null)
          {
              return NotFound();
          }
            //var orderModel = await _context.OrderModel.FindAsync(id);
            var orderModel = await _context.OrderModel.Include(o => o.Cart).ThenInclude(c=>c.SubCatMap).ThenInclude(s=>s.Product).FirstOrDefaultAsync(i => i.orderId == id);

            if (orderModel == null)
            {
                return NotFound();
            }

            return orderModel;
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderModel(int id, DaoOrder order)
        {
            //if (id != orderModel.orderId)
            //{
            //    return BadRequest();
            //}
            Console.WriteLine("\n\nentered api s put request  "+order.orderStatus);

            OrderModel orderModel = _context.OrderModel.FirstOrDefault(u => u.orderId== id);
            if (orderModel != null)
            {
                Console.WriteLine("model found");
                orderModel.cartId = order.cartId;
                orderModel.message = order.message;
                orderModel.paymentMode = order.paymentMode;
                orderModel.deliveryMode = order.deliveryMode;
                orderModel.amount = order.amount;
                orderModel.dateOrdered = order.dateOrdered;
                orderModel.orderStatus = order.orderStatus;
                _context.Entry(orderModel).State = EntityState.Modified;
            }
            _context.Entry(orderModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderModelExists(id))
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

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderModel>> PostOrderModel(DaoOrder order)
        {
          if (_context.OrderModel == null)
          {
              return Problem("Entity set 'Cake_Rush_APIContext.OrderModel'  is null.");
          }
            OrderModel orderModel = new OrderModel()
            {
                cartId= order.cartId,
                message= order.message,
                amount= order.amount,
                orderStatus= order.orderStatus,
                dateOrdered= order.dateOrdered,
                deliveryMode= order.deliveryMode,
                paymentMode= order.paymentMode,
                userId=order.userId
            };
            _context.OrderModel.Add(orderModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderModel", new { id = orderModel.orderId }, orderModel);
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderModel(int id)
        {
            if (_context.OrderModel == null)
            {
                return NotFound();
            }
            var orderModel = await _context.OrderModel.FindAsync(id);
            if (orderModel == null)
            {
                return NotFound();
            }

            _context.OrderModel.Remove(orderModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderModelExists(int id)
        {
            return (_context.OrderModel?.Any(e => e.orderId == id)).GetValueOrDefault();
        }
    }
}
