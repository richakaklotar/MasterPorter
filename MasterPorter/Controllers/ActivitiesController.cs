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
    public class ActivitiesController : ControllerBase
    {
        private readonly MasterPorterContext _context;

        public ActivitiesController(MasterPorterContext context)
        {
            _context = context;
        }

        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activities>>> GetActivities()
        {
          if (_context.Activities == null)
          {
              return NotFound();
          }
            return await _context.Activities.ToListAsync();
        }

        // GET: api/Activities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Activities>> GetActivities(int id)
        {
          if (_context.Activities == null)
          {
              return NotFound();
          }
            var activities = await _context.Activities.FindAsync(id);

            if (activities == null)
            {
                return NotFound();
            }

            return activities;
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivities(int id, Activities activities)
        {
            if (id != activities.ActivitiesID)
            {
                return BadRequest();
            }

            _context.Entry(activities).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivitiesExists(id))
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

        // POST: api/Activities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Activities>> PostActivities(Activities activities)
        {
          if (_context.Activities == null)
          {
              return Problem("Entity set 'MasterPorterContext.Activities'  is null.");
          }
            _context.Activities.Add(activities);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivities", new { id = activities.ActivitiesID }, activities);
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivities(int id)
        {
            if (_context.Activities == null)
            {
                return NotFound();
            }
            var activities = await _context.Activities.FindAsync(id);
            if (activities == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activities);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivitiesExists(int id)
        {
            return (_context.Activities?.Any(e => e.ActivitiesID == id)).GetValueOrDefault();
        }
    }
}
