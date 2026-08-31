using AutoEntity.EntityModels;
using Microsoft.AspNetCore.Mvc;
using ModifyService;
using ReadService;

namespace MasterPorter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(QPrimaryService.GetExistingActivitiesList());
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
                return Ok(QPrimaryService.GetActivities(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create(Activities activities)
        {
            try
            {
                activities.ActivitiesID = 0;

                var service = new DataModify();
                int id = service.SaveActivities(activities);

                return StatusCode(201,
                    QPrimaryService.GetActivities(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Activities activities)
        {
            try
            {
                QPrimaryService.GetActivities(id);

                activities.ActivitiesID = id;

                var service = new DataModify();
                service.SaveActivities(activities);

                return Ok(QPrimaryService.GetActivities(id));
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

                var activity = context.Activities
                    .FirstOrDefault(x => x.ActivitiesID == id);

                if (activity == null)
                    return NotFound();

                context.Activities.Remove(activity);
                context.SaveChanges();

                return Ok(new
                {
                    message = "Activity deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}