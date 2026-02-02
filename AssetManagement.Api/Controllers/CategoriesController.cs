using AssetManagement.Api.Data;
using AssetManagement.Api.DTOs;
using AssetManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound("Category not found");
            }

            return Ok(category);
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(SaveCategoryDto item)
        {
            var exists = await _context.Categories
                .AnyAsync(c => c.Name == item.Name.Trim());

            if (exists)
            {
                return Conflict("Category already exists");
            }

            var category = new Category
            {
                Name = item.Name.Trim()
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var result = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };

            return CreatedAtAction(nameof(GetCategory),
                new { id = category.Id }, result);
        }

        // PUT: api/Categories/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, SaveCategoryDto item)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound("Category not found");
            }

            var exists = await _context.Categories
                .AnyAsync(c => c.Name == item.Name.Trim() && c.Id != id);

            if (exists)
            {
                return Conflict("Category already exists");
            }

            category.Name = item.Name.Trim();

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Categories/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound("Category not found");
            }

            var hasAssets = await _context.Assets
                .AnyAsync(a => a.CategoryId == id);

            if (hasAssets)
            {
                return BadRequest("Cannot delete category with assets");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
