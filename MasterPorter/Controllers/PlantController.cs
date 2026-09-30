using AutoEntity.EntityModels;
using Microsoft.AspNetCore.Mvc;
using ModifyService;
using ReadService;

namespace MasterPorter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(QPrimaryService.GetPlantList());
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
                return Ok(QPrimaryService.GetPlant(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create(Plant plant)
        {
            try
            {
                plant.PlantId = 0;

                var service = new DataModify();
                int id = service.SavePlant(plant);

                return StatusCode(201, QPrimaryService.GetPlant(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Plant plant)
        {
            try
            {
                QPrimaryService.GetPlant(id);   // throws if not found

                plant.PlantId = id;

                var service = new DataModify();
                service.SavePlant(plant);

                return Ok(QPrimaryService.GetPlant(id));
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
                var service = new DataModify();

                if (!service.DeletePlant(id))
                    return NotFound(new { message = "Plant not found." });

                return Ok(new { message = "Plant deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}