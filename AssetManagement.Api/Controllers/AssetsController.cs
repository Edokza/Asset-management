using AssetManagement.Api.Data;
using AssetManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetManagement.Api.DTOs;

namespace AssetManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : Controller
    {
        private readonly AppDbContext _context;

        public AssetsController(AppDbContext context)
        {
            _context = context;
        }

        //[HttpGet("test-error")]
        //public IActionResult TestError()
        //{
        //    throw new Exception("Test error");
        //}

        // GET: api/assets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            var assets = await _context.Assets
                .Include(a => a.Category)
                .Select(a => new AssetDto{Id = a.Id,
                    Name = a.Name,
                    SerialNumber = a.SerialNumber,
                    CategoryId = a.CategoryId,
                    CategoryName = a.Category!.Name})
                .ToListAsync();

            return Ok(assets);

        }

        // POST: api/assets
        [HttpPost]
        public async Task<ActionResult<Asset>> CreateAsset(SaveAssetDto item)
        {

            // Check Category exists
            var categoryExists = await _context.Categories
                .AnyAsync(c => c.Id == item.CategoryId);

            if (!categoryExists)
            {
                return NotFound("Category not found");
            }

            var asset = new Asset
            {
                Name = item.Name.Trim(),
                SerialNumber = item.SerialNumber,
                CategoryId = item.CategoryId
            };

            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();

            var result = await _context.Assets
                .Include(a => a.Category)
                .Where(a => a.Id == asset.Id)
                .Select(a => new AssetDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    SerialNumber = a.SerialNumber,
                    CategoryId = a.CategoryId,
                    CategoryName = a.Category!.Name
                })
                .FirstAsync();

            return CreatedAtAction(nameof(GetAsset), new { id = asset.Id }, result);
        }

        // GET: api/assets/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(int id)
        {
            var assets = await _context.Assets
                .Include(a => a.Category)
                .Select(a => new AssetDto{
                    Id = a.Id,
                    Name = a.Name,
                    SerialNumber = a.SerialNumber,
                    CategoryId = a.CategoryId,
                    CategoryName = a.Category!.Name})
                .ToListAsync();

            return Ok(assets);

        }

        // PUT: api/assets/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsset(int id, SaveAssetDto item)
        {
            var asset = await _context.Assets.FindAsync(id);

            if (asset == null)
            {
                return NotFound("Asset not found");
            }

            var categoryExists = await _context.Categories
                .AnyAsync(c => c.Id == item.CategoryId);

            if (!categoryExists)
            {
                return NotFound("Category not found");
            }

            _context.Entry(asset).State = EntityState.Modified;

            asset.Name = item.Name.Trim();
            asset.SerialNumber = item.SerialNumber;
            asset.CategoryId = item.CategoryId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/assets/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            var asset = await _context.Assets.FindAsync(id);

            if (asset == null)
            {
                return NotFound("Asset not found");
            }

            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
