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