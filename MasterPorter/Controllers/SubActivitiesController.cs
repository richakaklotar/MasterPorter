using AutoEntity.EntityModels;
using Microsoft.AspNetCore.Mvc;
using ModifyService;
using ReadService;

namespace MasterPorter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubActivitiesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(QPrimaryService.GetExistingSubActivitiesList());
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
                return Ok(QPrimaryService.GetSubActivities(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create(SubActivities subactivities)
        {
            try
            {
                subactivities.SubActivitiesID = 0;

                var service = new DataModify();
                int id = service.SaveSubActivities(subactivities);

                return StatusCode(201,
                    QPrimaryService.GetSubActivities(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(
            int id,
            SubActivities subactivities)
        {
            try
            {
                QPrimaryService.GetSubActivities(id);

                subactivities.SubActivitiesID = id;

                var service = new DataModify();
                service.SaveSubActivities(subactivities);

                return Ok(
                    QPrimaryService.GetSubActivities(id));
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

                var item = context.SubActivities
                    .FirstOrDefault(x =>
                        x.SubActivitiesID == id);

                if (item == null)
                    return NotFound();

                context.SubActivities.Remove(item);
                context.SaveChanges();

                return Ok(new
                {
                    message =
                        "Sub Activity deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}