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
    public class SubCategoryMapController : ControllerBase
    {
        private readonly Cake_Rush_APIContext _context;

        public SubCategoryMapController(Cake_Rush_APIContext context)
        {
            _context = context;
        }

        // GET: api/SubCategoryMap
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryMapModel>>> GetSubCategoryMapModel()
        {
          if (_context.SubCategoryMapModel == null)
          {
              return NotFound();
          }
            //var subCatMap = await _context.SubCategoryMapModel.Join(_context.ProductModel, s => s.productId, p => p.productId, (s, p) => new
            //{
            //    s.categoryName,
            //    s.price,
            //    s.mapId,
            //    s.productId,
            //    product=new
            //    {
            //        p.productId,
            //        p.productName,
            //        p.productDescription,
            //        p.label
            //    }
            //}).ToListAsync();
            var subCatMap = await _context.SubCategoryMapModel.Include(o => o.Product).ToListAsync();
            foreach (var item in subCatMap)
            {
                if (item.Product == null)
                {
                    item.Product = await _context.ProductModel.FindAsync(item.productId);
                }
            }
            Console.WriteLine("--");
            return Ok(subCatMap);
            //return await _context.SubCategoryMapModel.ToListAsync();
        }

        // GET: api/SubCategoryMap/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoryMapModel>> GetSubCategoryMapModel(int id)
        {
          if (_context.SubCategoryMapModel == null)
          {
              return NotFound();
          }
            var subCategoryMapModel = await _context.SubCategoryMapModel.Include(o=>o.Product).FirstOrDefaultAsync(i=>i.mapId == id);

            if (subCategoryMapModel == null)
            {
                return NotFound();
            }

            return subCategoryMapModel;
        }

        // PUT: api/SubCategoryMap/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubCategoryMapModel(int id, SubCategoryMapModel subCategoryMapModel)
        {
            if (id != subCategoryMapModel.mapId)
            {
                return BadRequest();
            }

            _context.Entry(subCategoryMapModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoryMapModelExists(id))
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

        // POST: api/SubCategoryMap
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubCategoryMapModel>> PostSubCategoryMapModel([FromBody] DaoSubCat subcat)
        {
          if (_context.SubCategoryMapModel == null)
          {
              return Problem("Entity set 'Cake_Rush_APIContext.SubCategoryMapModel'  is null.");
          }
            SubCategoryMapModel subCategoryMapModel = new SubCategoryMapModel()
            {
                productId = subcat.productId,
                categoryName = subcat.categoryName,
                price = subcat.price
          };

            Console.WriteLine(subCategoryMapModel.productId+" "+subCategoryMapModel.categoryName+" "+subCategoryMapModel.price);
            _context.SubCategoryMapModel.Add(subCategoryMapModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubCategoryMapModel", new { id = subCategoryMapModel.mapId }, subCategoryMapModel);
        }

        // DELETE: api/SubCategoryMap/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategoryMapModel(int id)
        {
            if (_context.SubCategoryMapModel == null)
            {
                return NotFound();
            }
            var subCategoryMapModel = await _context.SubCategoryMapModel.FindAsync(id);
            if (subCategoryMapModel == null)
            {
                return NotFound();
            }

            _context.SubCategoryMapModel.Remove(subCategoryMapModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubCategoryMapModelExists(int id)
        {
            return (_context.SubCategoryMapModel?.Any(e => e.mapId == id)).GetValueOrDefault();
        }
    }
}
