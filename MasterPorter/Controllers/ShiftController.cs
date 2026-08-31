using AutoEntity.EntityModels;
using Microsoft.AspNetCore.Mvc;
using ModifyService;
using ReadService;

namespace MasterPorter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(QPrimaryService.GetExistingShiftList());
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
                return Ok(QPrimaryService.GetShift(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create(Shift shift)
        {
            try
            {
                shift.ShiftID = 0;

                var service = new DataModify();
                int id = service.SaveShift(shift);

                return StatusCode(201,
                    QPrimaryService.GetShift(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Shift shift)
        {
            try
            {
                QPrimaryService.GetShift(id);

                shift.ShiftID = id;

                var service = new DataModify();
                service.SaveShift(shift);

                return Ok(QPrimaryService.GetShift(id));
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

                var shift = context.Shift
                    .FirstOrDefault(x => x.ShiftID == id);

                if (shift == null)
                    return NotFound();

                context.Shift.Remove(shift);
                context.SaveChanges();

                return Ok(new
                {
                    message = "Shift deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}