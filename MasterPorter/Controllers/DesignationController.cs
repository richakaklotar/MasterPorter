using AutoEntity.EntityModels;
using Microsoft.AspNetCore.Mvc;
using ModifyService;
using ReadService;

namespace MasterPorter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(
                    QPrimaryService.GetExistingDesignationList());
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
                return Ok(
                    QPrimaryService.GetDesignation(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create(Designation designation)
        {
            try
            {
                designation.DesignationID = 0;

                var service = new DataModify();
                int id = service.SaveDesignation(designation);

                return StatusCode(
                    201,
                    QPrimaryService.GetDesignation(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(
            int id,
            Designation designation)
        {
            try
            {
                QPrimaryService.GetDesignation(id);

                designation.DesignationID = id;

                var service = new DataModify();
                service.SaveDesignation(designation);

                return Ok(
                    QPrimaryService.GetDesignation(id));
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

                var designation = context.Designation
                    .FirstOrDefault(x =>
                        x.DesignationID == id);

                if (designation == null)
                    return NotFound();

                context.Designation.Remove(designation);
                context.SaveChanges();

                return Ok(new
                {
                    message =
                        "Designation deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}