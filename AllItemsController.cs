using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User.Models;

namespace User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllItemsController : ControllerBase
    {
        private readonly Context _context;

        public AllItemsController(Context context)
        {
            _context = context;
        }

        // GET: api/AllItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Items>>> GetAllItems()
        {
            return await _context.AllItems.ToListAsync();
        }

        // GET: api/AllItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Items>> GetItems(long id)
        {
            var allItem = await _context.AllItems.FindAsync(id);

            if (allItem == null)
            {
                return NotFound();
            }

            return allItem;
        }

        // PUT: api/AllItems/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItems(long id, Items allItem)
        {
            if (id != allItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(allItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemsExists(id))
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

        // POST: api/AllItems
        [HttpPost]
        public async Task<ActionResult<Items>> PostItems(Items allItem)
        {
            _context.AllItems.Add(allItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetItems", new { id = allItem.Id }, allItem);
            return CreatedAtAction(nameof(GetItems), new { id = allItem.Id }, allItem);
        }

        // DELETE: api/AllItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Items>> DeleteItems(long id)
        {
            var allItem = await _context.AllItems.FindAsync(id);
            if (allItem == null)
            {
                return NotFound();
            }

            _context.AllItems.Remove(allItem);
            await _context.SaveChangesAsync();

            return allItem;
        }

        private bool ItemsExists(long id)
        {
            return _context.AllItems.Any(e => e.Id == id);
        }
    }
}
