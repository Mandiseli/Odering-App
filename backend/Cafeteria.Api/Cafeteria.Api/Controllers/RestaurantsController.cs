using Cafeteria.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public RestaurantsController(ApplicationDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _db.Restaurants.AsNoTracking().ToListAsync());

        [HttpGet("{id:int}/menu")]
        public async Task<IActionResult> GetMenu(int id) =>
            Ok(await _db.MenuItems.Where(m => m.RestaurantId == id && m.IsActive)
                                  .AsNoTracking().ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.Restaurant r)
        {
            _db.Restaurants.Add(r);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = r.Id }, r);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Models.Restaurant r)
        {
            if (id != r.Id) return BadRequest();
            _db.Entry(r).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var r = await _db.Restaurants.FindAsync(id);
            if (r == null) return NotFound();
            _db.Restaurants.Remove(r);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
