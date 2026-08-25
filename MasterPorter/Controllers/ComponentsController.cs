using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoEntity.EntityModels;

namespace MasterPorter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentsController : ControllerBase
    {
        private readonly MasterPorterContext _context;

        public ComponentsController(MasterPorterContext context)
        {
            _context = context;
        }

        // GET: api/Components
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Components>>> GetComponents()
        {
          if (_context.Components == null)
          {
              return NotFound();
          }
            return await _context.Components.ToListAsync();
        }

        // GET: api/Components/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Components>> GetComponents(int id)
        {
          if (_context.Components == null)
          {
              return NotFound();
          }
            var components = await _context.Components.FindAsync(id);

            if (components == null)
            {
                return NotFound();
            }

            return components;
        }

        // PUT: api/Components/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponents(int id, Components components)
        {
            if (id != components.ComponentID)
            {
                return BadRequest();
            }

            _context.Entry(components).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentsExists(id))
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

        // POST: api/Components
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Components>> PostComponents(Components components)
        {
          if (_context.Components == null)
          {
              return Problem("Entity set 'MasterPorterContext.Components'  is null.");
          }
            _context.Components.Add(components);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComponents", new { id = components.ComponentID }, components);
        }

        // DELETE: api/Components/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponents(int id)
        {
            if (_context.Components == null)
            {
                return NotFound();
            }
            var components = await _context.Components.FindAsync(id);
            if (components == null)
            {
                return NotFound();
            }

            _context.Components.Remove(components);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComponentsExists(int id)
        {
            return (_context.Components?.Any(e => e.ComponentID == id)).GetValueOrDefault();
        }
    }
}
