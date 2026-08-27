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
//    public class ProjectController : ControllerBase
//    {
//        private readonly MasterPorterContext _context;

//        public ProjectController(MasterPorterContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Project
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Project>>> GetProject()
//        {
//          if (_context.Project == null)
//          {
//              return NotFound();
//          }
//            return await _context.Project.ToListAsync();
//        }

//        // GET: api/Project/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Project>> GetProject(int id)
//        {
//          if (_context.Project == null)
//          {
//              return NotFound();
//          }
//            var project = await _context.Project.FindAsync(id);

//            if (project == null)
//            {
//                return NotFound();
//            }

//            return project;
//        }

//        // PUT: api/Project/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutProject(int id, Project project)
//        {
//            if (id != project.ProjectID)
//            {
//                return BadRequest();
//            }

//            _context.Entry(project).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ProjectExists(id))
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

//        // POST: api/Project
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Project>> PostProject(Project project)
//        {
//          if (_context.Project == null)
//          {
//              return Problem("Entity set 'MasterPorterContext.Project'  is null.");
//          }
//            _context.Project.Add(project);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetProject", new { id = project.ProjectID }, project);
//        }

//        // DELETE: api/Project/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteProject(int id)
//        {
//            if (_context.Project == null)
//            {
//                return NotFound();
//            }
//            var project = await _context.Project.FindAsync(id);
//            if (project == null)
//            {
//                return NotFound();
//            }

//            _context.Project.Remove(project);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool ProjectExists(int id)
//        {
//            return (_context.Project?.Any(e => e.ProjectID == id)).GetValueOrDefault();
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
