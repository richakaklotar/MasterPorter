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
