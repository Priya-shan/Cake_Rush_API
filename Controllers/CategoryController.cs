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
    public class CategoryController : ControllerBase
    {
        private readonly Cake_Rush_APIContext _context;

        public CategoryController(Cake_Rush_APIContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategoryModel()
        {
          if (_context.CategoryModel == null)
          {
              return NotFound();
          }
            return await _context.CategoryModel.ToListAsync();
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetCategoryModel(int id)
        {
          if (_context.CategoryModel == null)
          {
              return NotFound();
          }
            var categoryModel = await _context.CategoryModel.FindAsync(id);

            if (categoryModel == null)
            {
                return NotFound();
            }

            return categoryModel;
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryModel(int id, CategoryModel categoryModel)
        {
            if (id != categoryModel.categoryId)
            {
                return BadRequest();
            }

            _context.Entry(categoryModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryModelExists(id))
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

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]

        public async Task<ActionResult<CategoryModel>> PostCategoryModel([FromBody] DaoCategory category)
        {
          if (_context.CategoryModel == null)
          {
              return Problem("Entity set 'Cake_Rush_APIContext.CategoryModel'  is null.");
          }
          
          CategoryModel categoryModel=new CategoryModel() { categoryName=category.categoryName};
            _context.CategoryModel.Add(categoryModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryModel", new { id = categoryModel.categoryId }, categoryModel);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryModel(int id)
        {
            if (_context.CategoryModel == null)
            {
                return NotFound();
            }
            var categoryModel = await _context.CategoryModel.FindAsync(id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            _context.CategoryModel.Remove(categoryModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryModelExists(int id)
        {
            return (_context.CategoryModel?.Any(e => e.categoryId == id)).GetValueOrDefault();
        }
    }
}
