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
    public class DivisionController : ControllerBase
    {
        private readonly MasterPorterContext _context;

        public DivisionController(MasterPorterContext context)
        {
            _context = context;
        }

        // GET: api/Division
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Division>>> GetDivision()
        {
          if (_context.Division == null)
          {
              return NotFound();
          }
            return await _context.Division.ToListAsync();
        }

        // GET: api/Division/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Division>> GetDivision(int id)
        {
          if (_context.Division == null)
          {
              return NotFound();
          }
            var division = await _context.Division.FindAsync(id);

            if (division == null)
            {
                return NotFound();
            }

            return division;
        }

        // PUT: api/Division/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDivision(int id, Division division)
        {
            if (id != division.DivisionId)
            {
                return BadRequest();
            }

            _context.Entry(division).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DivisionExists(id))
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

        // POST: api/Division
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Division>> PostDivision(Division division)
        {
          if (_context.Division == null)
          {
              return Problem("Entity set 'MasterPorterContext.Division'  is null.");
          }
            _context.Division.Add(division);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDivision", new { id = division.DivisionId }, division);
        }

        // DELETE: api/Division/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDivision(int id)
        {
            if (_context.Division == null)
            {
                return NotFound();
            }
            var division = await _context.Division.FindAsync(id);
            if (division == null)
            {
                return NotFound();
            }

            _context.Division.Remove(division);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DivisionExists(int id)
        {
            return (_context.Division?.Any(e => e.DivisionId == id)).GetValueOrDefault();
        }
    }
}
