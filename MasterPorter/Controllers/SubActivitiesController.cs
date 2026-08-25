using AutoEntity.EntityModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterPorter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowReactApp")]
    public class SubActivitiesController : ControllerBase
    {
        private readonly MasterPorterContext _context;

        public SubActivitiesController(MasterPorterContext context)
        {
            _context = context;
        }

        // GET: api/SubActivities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubActivities>>> GetSubActivities()
        {
          if (_context.SubActivities == null)
          {
              return NotFound();
          }
            return await _context.SubActivities.ToListAsync();
        }

        // GET: api/SubActivities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubActivities>> GetSubActivities(int id)
        {
          if (_context.SubActivities == null)
          {
              return NotFound();
          }
            var subActivities = await _context.SubActivities.FindAsync(id);

            if (subActivities == null)
            {
                return NotFound();
            }

            return subActivities;
        }

        // PUT: api/SubActivities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubActivities(int id, SubActivities subActivities)
        {
            if (id != subActivities.SubActivitiesID)
            {
                return BadRequest();
            }

            _context.Entry(subActivities).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubActivitiesExists(id))
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

        // POST: api/SubActivities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubActivities>> PostSubActivities(SubActivities subActivities)
        {
          if (_context.SubActivities == null)
          {
              return Problem("Entity set 'MasterPorterContext.SubActivities'  is null.");
          }
            _context.SubActivities.Add(subActivities);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubActivities", new { id = subActivities.SubActivitiesID }, subActivities);
        }

        // DELETE: api/SubActivities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubActivities(int id)
        {
            if (_context.SubActivities == null)
            {
                return NotFound();
            }
            var subActivities = await _context.SubActivities.FindAsync(id);
            if (subActivities == null)
            {
                return NotFound();
            }

            _context.SubActivities.Remove(subActivities);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubActivitiesExists(int id)
        {
            return (_context.SubActivities?.Any(e => e.SubActivitiesID == id)).GetValueOrDefault();
        }
    }
}
