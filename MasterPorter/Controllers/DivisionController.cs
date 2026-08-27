//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using AutoEntity.EntityModels;

//namespace MasterPorter.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class DivisionController : ControllerBase
//    {
//        private readonly MasterPorterContext _context;

//        public DivisionController(MasterPorterContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Division
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Division>>> GetDivision()
//        {
//          if (_context.Division == null)
//          {
//              return NotFound();
//          }
//            return await _context.Division.ToListAsync();
//        }

//        // GET: api/Division/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Division>> GetDivision(int id)
//        {
//          if (_context.Division == null)
//          {
//              return NotFound();
//          }
//            var division = await _context.Division.FindAsync(id);

//            if (division == null)
//            {
//                return NotFound();
//            }

//            return division;
//        }

//        // PUT: api/Division/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutDivision(int id, Division division)
//        {
//            if (id != division.DivisionId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(division).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!DivisionExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Division
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Division>> PostDivision(Division division)
//        {
//          if (_context.Division == null)
//          {
//              return Problem("Entity set 'MasterPorterContext.Division'  is null.");
//          }
//            _context.Division.Add(division);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetDivision", new { id = division.DivisionId }, division);
//        }

//        // DELETE: api/Division/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteDivision(int id)
//        {
//            if (_context.Division == null)
//            {
//                return NotFound();
//            }
//            var division = await _context.Division.FindAsync(id);
//            if (division == null)
//            {
//                return NotFound();
//            }

//            _context.Division.Remove(division);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool DivisionExists(int id)
//        {
//            return (_context.Division?.Any(e => e.DivisionId == id)).GetValueOrDefault();
//        }
//    }
//}


using AutoEntity.EntityModels;
using Microsoft.AspNetCore.Mvc;
using ModifyService;
using ReadService;

namespace MasterPorter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(QPrimaryService.GetExistingDivisionList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(QPrimaryService.GetDivision(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create(Division division)
        {
            try
            {
                division.DivisionId = 0;

                var service = new DataModify();
                int id = service.SaveDivision(division);

                return StatusCode(201,
                    QPrimaryService.GetDivision(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Division division)
        {
            try
            {
                QPrimaryService.GetDivision(id);

                division.DivisionId = id;

                var service = new DataModify();
                service.SaveDivision(division);

                return Ok(QPrimaryService.GetDivision(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using var context = new MasterPorterContext();

                var division = context.Division
                    .FirstOrDefault(x => x.DivisionId == id);

                if (division == null)
                    return NotFound();

                context.Division.Remove(division);
                context.SaveChanges();

                return Ok(new
                {
                    message = "Division deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
