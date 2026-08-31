using AutoEntity.EntityModels;
using Microsoft.AspNetCore.Mvc;
using ModifyService;
using ReadService;

namespace MasterPorter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(
                    QPrimaryService.GetExistingEmployeeList());
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
                    QPrimaryService.GetEmployee(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            try
            {
                if (employee == null)
                    return BadRequest(new
                    {
                        message = "Employee data is required."
                    });

                employee.EmployeeID = 0;

                var service = new DataModify();

                int id = service.SaveEmployee(employee);

                return StatusCode(
                    201,
                    QPrimaryService.GetEmployee(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    innerException =
                        ex.InnerException?.Message
                });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(
            int id,
            Employee employee)
        {
            try
            {
                QPrimaryService.GetEmployee(id);

                employee.EmployeeID = id;

                var service = new DataModify();

                service.SaveEmployee(employee);

                return Ok(
                    QPrimaryService.GetEmployee(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    innerException =
                        ex.InnerException?.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using var context = new MasterPorterContext();

                var employee = context.Employee
                    .FirstOrDefault(x =>
                        x.EmployeeID == id);

                if (employee == null)
                {
                    return NotFound(new
                    {
                        message = "Employee not found."
                    });
                }

                context.Employee.Remove(employee);
                context.SaveChanges();

                return Ok(new
                {
                    message =
                        "Employee deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    innerException =
                        ex.InnerException?.Message
                });
            }
        }
    }
}