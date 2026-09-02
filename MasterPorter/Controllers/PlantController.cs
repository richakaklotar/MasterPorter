using Microsoft.AspNetCore.Mvc;
using AutoEntity.EntityModels;
using ModifyService;
using ReadService;

namespace MasterPorter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        // =====================================================
        // GET ALL
        // GET: api/Plant
        // =====================================================
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var plants = QPrimaryService.GetExistingPlantList();
                return Ok(plants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // =====================================================
        // GET BY ID
        // GET: api/Plant/1
        // =====================================================
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { message = "Invalid Plant ID." });
                }

                var plant = QPrimaryService.GetPlant(id);

                if (plant == null)
                {
                    return BadRequest(new { message = "Plant not found." });
                }

                return Ok(plant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // =====================================================
        // INSERT
        // POST: api/Plant
        // =====================================================
        [HttpPost]
        public IActionResult Create([FromBody] Plant plant)
        {
            try
            {
                if (plant == null)
                {
                    return BadRequest(new { message = "Plant data is required." });
                }

                if (string.IsNullOrWhiteSpace(plant.PlantName))
                {
                    return BadRequest(new { message = "Plant Name is required." });
                }

                if (string.IsNullOrWhiteSpace(plant.PlantCode))
                {
                    return BadRequest(new { message = "Plant Code is required." });
                }

                plant.PlantId = 0;

                var service = new DataModify();
                var result = service.SaveBusiness(plant);

                if (result <= 0)
                {
                    return BadRequest(new { message = "Plant could not be inserted." });
                }

                return Ok(new
                {
                    message = "Plant inserted successfully.",
                    plantId = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message,
                    innerException = ex.InnerException?.Message
                });
            }
        }
        
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Plant plant)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { message = "Invalid Plant ID." });
                }

                if (plant == null)
                {
                    return BadRequest(new { message = "Plant data is required." });
                }

                if (string.IsNullOrWhiteSpace(plant.PlantName))
                {
                    return BadRequest(new { message = "Plant Name is required." });
                }

                if (string.IsNullOrWhiteSpace(plant.PlantCode))
                {
                    return BadRequest(new { message = "Plant Code is required." });
                }

                // Assign route parameter ID directly to the entity
                plant.PlantId = id;
                plant.PlantName = plant.PlantName.Trim();
                plant.PlantCode = plant.PlantCode.Trim();

                var service = new DataModify();
                var result = service.SaveBusiness(plant);

                if (result <= 0)
                {
                    return BadRequest(new { message = "Plant could not be updated or record was not found." });
                }

                return Ok(new
                {
                    message = "Plant updated successfully.",
                    plantId = id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message,
                    innerException = ex.InnerException?.Message
                });
            }
        }

        // =====================================================
        // DELETE
        // DELETE: api/Plant/1
        // =====================================================
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { message = "Invalid Plant ID." });
                }

                var existingPlant = QPrimaryService.GetPlant(id);

                if (existingPlant == null)
                {
                    return BadRequest(new { message = "Plant not found." });
                }

                var service = new DataModify();
                var result = service.DeletePlant(id);

                if (!result)
                {
                    return BadRequest(new { message = "Plant could not be deleted." });
                }

                return Ok(new { message = "Plant deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message,
                    innerException = ex.InnerException?.Message
                });
            }
        }
    }
}