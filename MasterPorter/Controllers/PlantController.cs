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
                return Ok(QPrimaryService.GetExistingPlantList());
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
                int id = service.SaveBusiness(plant);

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
                QPrimaryService.GetPlant(id);

                plant.PlantId = id;

                var service = new DataModify();
                service.SaveBusiness(plant);

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
                using var context = new MasterPorterContext();

                var plant = context.Plants
                    .FirstOrDefault(x => x.PlantId == id);

                if (plant == null)
                    return NotFound();

                context.Plants.Remove(plant);
                context.SaveChanges();

                return Ok(new
                {
                    message = "Plant deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}