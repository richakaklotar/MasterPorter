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
//    public class MachineController : ControllerBase
//    {
//        private readonly MasterPorterContext _context;

//        public MachineController(MasterPorterContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Machine
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Machine>>> GetMachine()
//        {
//          if (_context.Machine == null)
//          {
//              return NotFound();
//          }
//            return await _context.Machine.ToListAsync();
//        }

//        // GET: api/Machine/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Machine>> GetMachine(int id)
//        {
//          if (_context.Machine == null)
//          {
//              return NotFound();
//          }
//            var machine = await _context.Machine.FindAsync(id);

//            if (machine == null)
//            {
//                return NotFound();
//            }

//            return machine;
//        }

//        // PUT: api/Machine/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutMachine(int id, Machine machine)
//        {
//            if (id != machine.MachineID)
//            {
//                return BadRequest();
//            }

//            _context.Entry(machine).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!MachineExists(id))
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

//        // POST: api/Machine
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Machine>> PostMachine(Machine machine)
//        {
//          if (_context.Machine == null)
//          {
//              return Problem("Entity set 'MasterPorterContext.Machine'  is null.");
//          }
//            _context.Machine.Add(machine);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetMachine", new { id = machine.MachineID }, machine);
//        }

//        // DELETE: api/Machine/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteMachine(int id)
//        {
//            if (_context.Machine == null)
//            {
//                return NotFound();
//            }
//            var machine = await _context.Machine.FindAsync(id);
//            if (machine == null)
//            {
//                return NotFound();
//            }

//            _context.Machine.Remove(machine);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool MachineExists(int id)
//        {
//            return (_context.Machine?.Any(e => e.MachineID == id)).GetValueOrDefault();
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
    public class MachineController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(QPrimaryService.GetExistingMachineList());
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
                return Ok(QPrimaryService.GetMachine(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create(Machine machine)
        {
            try
            {
                machine.MachineID = 0;

                var service = new DataModify();
                int id = service.SaveMachine(machine);

                return StatusCode(201,
                    QPrimaryService.GetMachine(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Machine machine)
        {
            try
            {
                QPrimaryService.GetMachine(id);

                machine.MachineID = id;

                var service = new DataModify();
                service.SaveMachine(machine);

                return Ok(QPrimaryService.GetMachine(id));
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

                var machine = context.Machine
                    .FirstOrDefault(x => x.MachineID == id);

                if (machine == null)
                    return NotFound();

                context.Machine.Remove(machine);
                context.SaveChanges();

                return Ok(new
                {
                    message = "Machine deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
