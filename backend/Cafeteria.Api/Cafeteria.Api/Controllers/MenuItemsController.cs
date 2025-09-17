using Cafeteria.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public MenuItemsController(ApplicationDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.MenuItem item)
        {
            _db.MenuItems.Add(item);
            await _db.SaveChangesAsync();
            return Created($"api/restaurants/{item.RestaurantId}/menu", item);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Models.MenuItem item)
        {
            if (id != item.Id) return BadRequest();
            _db.Entry(item).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var mi = await _db.MenuItems.FindAsync(id);
            if (mi == null) return NotFound();
            _db.MenuItems.Remove(mi);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
