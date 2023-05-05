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
    public class ProductController : ControllerBase
    {
        private readonly Cake_Rush_APIContext _context;

        public ProductController(Cake_Rush_APIContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductModel()
        {
          if (_context.ProductModel == null)
          {
              return NotFound();
          }
            return await _context.ProductModel.ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductModel(int id)
        {
          if (_context.ProductModel == null)
          {
              return NotFound();
          }
            var productModel = await _context.ProductModel.FindAsync(id);

            if (productModel == null)
            {
                return NotFound();
            }

            return productModel;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductModel(int id, ProductModel productModel)
        {
            if (id != productModel.productId)
            {
                return BadRequest();
            }

            _context.Entry(productModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductModelExists(id))
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

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductModel>> PostProductModel([FromBody] DaoProduct product)
        {
            //Console.WriteLine("within api -> "+product.price + " " + product.categoryId);
          if (_context.ProductModel == null)
          {
              return Problem("Entity set 'Cake_Rush_APIContext.ProductModel'  is null.");
          }
          ProductModel productModel =new ProductModel() { 
              categoryId=product.categoryId,
              productName=product.productName,
              productDescription=product.productDescription,
              label=product.label,
              price=product.price,
              imageId=product.imageid
          };
            _context.ProductModel.Add(productModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductModel", new { id = productModel.productId }, productModel);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductModel(int id)
        {
            if (_context.ProductModel == null)
            {
                return NotFound();
            }
            var productModel = await _context.ProductModel.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }

            _context.ProductModel.Remove(productModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductModelExists(int id)
        {
            return (_context.ProductModel?.Any(e => e.productId == id)).GetValueOrDefault();
        }
    }
}
