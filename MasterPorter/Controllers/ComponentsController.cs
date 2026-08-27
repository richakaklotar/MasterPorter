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
//    public class ComponentsController : ControllerBase
//    {
//        private readonly MasterPorterContext _context;

//        public ComponentsController(MasterPorterContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Components
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Components>>> GetComponents()
//        {
//          if (_context.Components == null)
//          {
//              return NotFound();
//          }
//            return await _context.Components.ToListAsync();
//        }

//        // GET: api/Components/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Components>> GetComponents(int id)
//        {
//          if (_context.Components == null)
//          {
//              return NotFound();
//          }
//            var components = await _context.Components.FindAsync(id);

//            if (components == null)
//            {
//                return NotFound();
//            }

//            return components;
//        }

//        // PUT: api/Components/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutComponents(int id, Components components)
//        {
//            if (id != components.ComponentID)
//            {
//                return BadRequest();
//            }

//            _context.Entry(components).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ComponentsExists(id))
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

//        // POST: api/Components
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Components>> PostComponents(Components components)
//        {
//          if (_context.Components == null)
//          {
//              return Problem("Entity set 'MasterPorterContext.Components'  is null.");
//          }
//            _context.Components.Add(components);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetComponents", new { id = components.ComponentID }, components);
//        }

//        // DELETE: api/Components/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteComponents(int id)
//        {
//            if (_context.Components == null)
//            {
//                return NotFound();
//            }
//            var components = await _context.Components.FindAsync(id);
//            if (components == null)
//            {
//                return NotFound();
//            }

//            _context.Components.Remove(components);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool ComponentsExists(int id)
//        {
//            return (_context.Components?.Any(e => e.ComponentID == id)).GetValueOrDefault();
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
    public class ComponentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(QPrimaryService.GetExistingComponentsList());
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
                return Ok(QPrimaryService.GetComponents(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create(Components components)
        {
            try
            {
                components.ComponentID = 0;

                var service = new DataModify();
                int id = service.SaveComponents(components);

                return StatusCode(201,
                    QPrimaryService.GetComponents(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Components components)
        {
            try
            {
                QPrimaryService.GetComponents(id);

                components.ComponentID = id;

                var service = new DataModify();
                service.SaveComponents(components);

                return Ok(QPrimaryService.GetComponents(id));
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

                var component = context.Components
                    .FirstOrDefault(x => x.ComponentID == id);

                if (component == null)
                    return NotFound();

                context.Components.Remove(component);
                context.SaveChanges();

                return Ok(new
                {
                    message = "Component deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
