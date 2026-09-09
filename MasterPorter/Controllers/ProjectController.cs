using AutoEntity.EntityModels;
using Microsoft.AspNetCore.Mvc;
using ModifyService;
using ReadService;

namespace MasterPorter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(QPrimaryService.GetExistingProjectList());
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
                return Ok(QPrimaryService.GetProject(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create(Project project)
        {
            try
            {
                project.ProjectID = 0;

                var service = new DataModify();
                int id = service.SaveProject(project);

                return StatusCode(201,
                    QPrimaryService.GetProject(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Project project)
        {
            try
            {
                QPrimaryService.GetProject(id);

                project.ProjectID = id;

                var service = new DataModify();
                service.SaveProject(project);

                return Ok(QPrimaryService.GetProject(id));
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

                var project = context.Project
                    .FirstOrDefault(x => x.ProjectID == id);

                if (project == null)
                    return NotFound();

                context.Project.Remove(project);
                context.SaveChanges();

                return Ok(new
                {
                    message = "Project deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
